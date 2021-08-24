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

public class AWSManager : MonoBehaviour
{
    private AWSManager _instance;
    public AWSManager Instance
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
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        S3Client.ListBucketsAsync(new ListBucketsRequest(), (responsObject) =>
        {

            if(responsObject.Exception == null)
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
}
