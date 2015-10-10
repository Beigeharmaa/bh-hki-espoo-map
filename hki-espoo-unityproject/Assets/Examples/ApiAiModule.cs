//
// API.AI Unity SDK Sample
// =================================================
//
// Copyright (C) 2015 by Speaktoit, Inc. (https://www.speaktoit.com)
// https://www.api.ai
//
// ***********************************************************************************************************************
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
//
// ***********************************************************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ApiAiSDK;
using ApiAiSDK.Model;
using ApiAiSDK.Unity;
using Newtonsoft.Json;
using System.Net;
using SimpleJSON;

public class ApiAiModule : MonoBehaviour
{

    private ApiAiUnity apiAiUnity;
    private AudioSource aud;
    public AudioClip sorryNoResponse;
    public AudioClip sorryRepeat;
    private AudioSource sorryNoResponseSource;
    private AudioSource sorryRepeatSource;
    BinaryWriter writer;
    FileStream fs;
    int blocksWrote;
    public int counter;
    public Dictionary<string, Landmark> landmarks;
    private bool recordingMode;
    public Soundbank soundbank;

    private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
    { 
        NullValueHandling = NullValueHandling.Ignore,
    };

    // Use this for initialization
    IEnumerator Start()
    {

		// Init MoveCamera location
		MoveCamera moveCamera = GetComponent<MoveCamera>();
		moveCamera.UpdateLocation("espoo");

		// check access to the Microphone
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            throw new NotSupportedException("Microphone using not authorized");
        }

        ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) =>
        {
            return true;
        };

        const string SUBSCRIPTION_KEY = "8a414957-b4a9-45e7-8af7-d942f77ff7ff";
        const string ACCESS_TOKEN = "7cbe30ec8f6342fbb2c2e8baa80dec3e";

        var config = new AIConfiguration(SUBSCRIPTION_KEY, ACCESS_TOKEN, SupportedLanguage.English);

        apiAiUnity = new ApiAiUnity();
        apiAiUnity.Initialize(config);

        apiAiUnity.OnError += HandleOnError;
        apiAiUnity.OnResult += HandleOnResult;

        landmarks = new Dictionary<string, Landmark>();
        
        landmarks.Add( "Helsinki Cathedral",          new Landmark("Helsinki Cathedral",         "church"));
        landmarks.Add( "Design Factory",              new Landmark("Design Factory", "espoo"));
        landmarks.Add( "Fortress of Finland",        new Landmark("Fortress of Finland", "suomenlinna"));
		landmarks.Add( "FJORD Helsinki",              new Landmark("FJORD Helsinki",             "fjord"));

        //landmarks.Add( "The University of Helsinki",  new Landmark("The University of Helsinki", "university"));
        //landmarks.Add( "The Senate Square",           new Landmark("The Senate Square",          "square"));
    }


    void showLandmark(string key)
    {
      Debug.Log ("Show landmark: " + key);

      UI ui = GetComponent<UI>();
      ui.setText(key);

      Landmark landmark = landmarks[key];

      // -> camera
      // -> camera target
      // todo : animate main camera to new camera position
      string container = landmark.getContainer();
      MoveCamera moveCamera = GetComponent<MoveCamera>();
      moveCamera.UpdateLocation(container);

      Debug.Log ("Landmark object:");
      Debug.Log (container);
      Debug.Log (landmark.getName ());

    }

    void HandleOnResult(object sender, AIResponseEventArgs e)
    {
        var aiResponse = e.Response;
        if (aiResponse != null)
        {
          Debug.Log("ResolvedQuery");
          Debug.Log(aiResponse.Result.ResolvedQuery);
          //Debug.Log("Result:");
          //Debug.Log(aiResponse.Result);
          //Debug.Log("Parameters:");
          //Debug.Log(aiResponse.Result.Parameters);
          
          // SCENARIO ONE: FAIL: Misheard sentence, no result.parameters
          // SCENARIO TWO: SUCCESS: We have a landmark in the result.parameters

          if(aiResponse.Result.Parameters != null)
          {
            string key  = aiResponse.Result.Parameters["Landmark"];
            string city = aiResponse.Result.Parameters["geo-city"];
            if (key == "Thank you") {
              soundbank.thankYou();
            } else {
              showLandmark(key);
            }
          } else {
            Debug.Log("Response, but no landmark recognized");
            
            soundbank.repeat();
          }
      
        } else
        {
            Debug.LogError("Response is null");
            soundbank.noResponse ();
        }
    }
    
    void HandleOnError(object sender, AIErrorEventArgs e)
    {
        Debug.LogException(e.Exception);
        Debug.Log(e.ToString());
    }
    
    // Update is called once per frame
    void Update()
    {
      if (apiAiUnity != null)
      {
          apiAiUnity.Update();
      }
      if(Input.GetKeyUp(KeyCode.Space)) {
        recordingMode = !recordingMode;
        
        if(recordingMode)
        {
          Debug.Log ("START RECORDING ");
          StartListening ();
        } else {
          Debug.Log ("STOP RECORDING ");
          StopListening ();
        }
      }
    }
    
    public void PluginInit()
    {
        
    }

    // Called by button
    // Call by SPACE key
    public void StartListening()
    {
        Debug.Log("StartListening");
        aud = GetComponent<AudioSource>();
        apiAiUnity.StartListening(aud);
        // todo : show visual feedback on listening to voice
    
    }
    
  // Called by button
  // Call by SPACE key
  public void StopListening()
    {
      // todo : hide visual feedback on listening to voice
        try
        {
            Debug.Log("StopListening");            
            apiAiUnity.StopListening();
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    
}
