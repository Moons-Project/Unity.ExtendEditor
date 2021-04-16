﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class SystemManager : MonoBehaviour {
  public GameObject pickUpHolder;
  public TextMeshProUGUI Score;
  public TextMeshProUGUI Level;
  public LevelData levelData;
  public NotificationManager myNotification;

  private int pickUpNumber = 0;
  private int totalNumber = 0;
  private int levelNumber = 0;
  private const float mapSize = 4.0f;
  private const string ScoreText = "Score: ";
  private const string LevelText = "Level: ";

  public float MapSize { get { return mapSize; } }

  private void Reset() {
    Debug.Log("Reset");
  }

  private void Awake() {
    Debug.Log("Awake");
  }

  private void Start() {
    levelData = new LevelData(Application.dataPath);
    notification("Start", "Enjoy The Game");
    notification("Start", "Enjoy The Game");


   // myNotification.CloseWindow(); // Close notification
    InitialLevelText();
    InitialScoreText();
    InitialPickUps();

    // StartAsync();
  }

  private void AddScore() {
    pickUpNumber += 1;
    notification("Great", $"you picked {pickUpNumber}");
    UpdateScoreText();
    CheckLevelFinish();
  }

  private void InitialPickUps() {
    if (levelNumber < levelData.LevelCount) {
      var eachLevel = levelData[levelNumber];
      // set total number
      totalNumber = eachLevel.Count;

      for (int i = 0; i < totalNumber; i++) {
        var eachBox = eachLevel[i];
        Vector3 position = new Vector3(eachBox.x_percent * mapSize,
          0.75f, eachBox.z_percent * mapSize);
        Instantiate(
          Resources.Load("Prefabs/Cube"), position, new Quaternion(),
          pickUpHolder.transform);
      }
      CheckLevelFinish();
    } else {
      notification("Congratulation!", $"you pass the game");
      // Debug.Break();
    }
  }

  private void CheckLevelFinish() {
    if (pickUpNumber == totalNumber) {
      notification("Level Up!", $"Congratulation");

      levelNumber += 1;
      UpdateLevelText();
      InitialScoreText();
      InitialPickUps();
    }
  }

  private void notification(string title, string content) {
    var newNotification = Instantiate(myNotification);
    newNotification.transform.position = myNotification.transform.position;
    newNotification.transform.SetParent(myNotification.transform.parent);
    // newNotification.transform.parent = myNotification.transform.parent;
    newNotification.title = title;
    newNotification.description = content;
    newNotification.useStacking = true;
    newNotification.UpdateUI(); // Update UI
    newNotification.OpenNotification(); // Open notification
  }

  private void InitialScoreText() {
    pickUpNumber = 0;
    UpdateScoreText();
  }

  private void UpdateScoreText() {
    Score.text = ScoreText + pickUpNumber.ToString();
  }

  private void InitialLevelText() {
    levelNumber = 0;
    UpdateLevelText();
  }

  private void UpdateLevelText() {
    Level.text = LevelText + (levelNumber + 1).ToString();
  }

  private async void StartAsync() {
    // Test Linq
    await Task.Run(() => {
      Enumerable.Range(0, 100)
        .Select(i => (new System.Random(i)).Next())
        .Where(x => x > 0 && x % 2 == 0)
        .Select(x => x % 10)
        .OrderBy(x => x)
        .ToList()
        .ForEach(new Action<int>(x => {
          //   Debug.Log(x.ToString() + " ");
        }));
    });
  }
}