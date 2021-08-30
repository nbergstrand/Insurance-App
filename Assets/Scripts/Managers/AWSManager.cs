using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class AWSManager : MonoBehaviour
{
    #region Singleton
    private static AWSManager _instance;
    public static AWSManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("AWSManager is NULL");
            }

            return _instance;
        }
    }
    #endregion

    public string S3Region = RegionEndpoint.EUWest2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }


    private AmazonS3Client _s3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if(_s3Client == null)
            {
                _s3Client = new AmazonS3Client( new CognitoAWSCredentials(
                "eu-west-2:05f52d30-1112-425c-8d31-291b8cf385a7", // Identity pool ID
                RegionEndpoint.EUWest2 // Region
                 ), _S3Region);
            }

            return _s3Client;
        }
    }
    


    private void Awake()
    {
        _instance = this;

        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        TestAccess();

    }

    private void TestAccess()
    {

        S3Client.ListBucketsAsync(new ListBucketsRequest(), (responsObject) =>
        {

            if (responsObject.Exception == null)
            {
                responsObject.Response.Buckets.ForEach((s3b) =>
                {
                    Debug.Log("Bucket Name: " + s3b.BucketName);
                });

            }
            else
            {
                Debug.Log("AWS Error: " + responsObject.Exception);
            }


        });
    }

    public void UploadToS3(string filePath, string fileName)
    {
        FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

        PostObjectRequest request = new PostObjectRequest()
        {
            Bucket = "aws-insurance-app-bucket",
            Key = fileName,
            InputStream = stream,
            CannedACL = S3CannedACL.Private,
            Region = _S3Region
        };

        S3Client.PostObjectAsync(request, (responeObj) =>
        {

            if (responeObj.Exception == null)
            {
                Debug.Log("Successful upload");
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("Upload exception:" + responeObj.Exception);
            }
        });
    }

    public void LoadFromS3(string caseNumber, Action onComplete = null)
    {
        string target = "Case #" + caseNumber; 

        var request = new ListObjectsRequest()
        {
            BucketName = "aws-insurance-app-bucket"
        };

        S3Client.ListObjectsAsync(request, (responsObject) =>
       {
           if (responsObject.Exception == null)
           {
               bool caseFound = responsObject.Response.S3Objects.Any(obj => obj.Key == target);
               
               if(caseFound == true)
               {
                   Debug.Log("Found case!");
                   S3Client.GetObjectAsync("aws-insurance-app-bucket", target, (responsObj) =>
                   {
                       if(responsObj.Response.ResponseStream != null)
                       {
                           byte[] data = null;

                           using (StreamReader reader = new StreamReader(responsObj.Response.ResponseStream))
                           {
                               using (MemoryStream memory = new MemoryStream())
                               {
                                   var buffer = new byte[512];
                                   var bytesRead = default(int);

                                   while((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                                   {
                                       memory.Write(buffer, 0, bytesRead);
                                   }

                                   data = memory.ToArray();
                               }
                                                                
                           }

                           using (MemoryStream memory = new MemoryStream(data))
                           {
                               BinaryFormatter formatter = new BinaryFormatter();
                               UIManager.Instance.activeCase = (Case)formatter.Deserialize(memory);

                               if(onComplete != null)
                                    onComplete();
                           }
                       }


                   });
               }
               else
               {
                   Debug.Log("No case found");
               }
           }
       });

    }
}
