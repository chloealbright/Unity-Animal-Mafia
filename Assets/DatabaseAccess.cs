
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System;

public class DatabaseAccess : MonoBehaviour
{

    MongoClient client = new MongoClient("mongodb+srv://temp:WEXU2lo6XGlO94l6@farm.yz9bedw.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

  // Start is called before the first frame update
  void Start()
    {
        database = client.GetDatabase("HighScoreDB");
        collection = database.GetCollection<BsonDocument>("HighScoreCollection");

        // Test statement
        // var document = new BsonDocument{{"username", 100}};
        // collection.InsertOne(document);

        //Testing statement: Call the database and get all the data. 
        GetScoresFromDataBase();

        SaveScoreToDataBase("MrPineApple", PlayerPrefs.GetInt("ScoreValue"));
     
    }

    // Update is called once per frame
    public async void SaveScoreToDataBase(string userName, int score)
    {
        var document = new BsonDocument{{userName, score}};
        await collection.InsertOneAsync(document);

    }

    public async Task<List<HighScore>> GetScoresFromDataBase()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;

        List<HighScore> highscores = new List<HighScore>();
        foreach(var score in scoresAwaited.ToList())
        {
            highscores.Add(Deserialize(score.ToString()));
        }
        return highscores;
    }

    //Extracting important information from the json using basic string manipulation
    private HighScore Deserialize(string rawJson)
    {
        

        var highScore = new HighScore();
        //Remove ID
        var stringWithoutID = rawJson.Substring(rawJson.IndexOf("),")+4);
        var username = stringWithoutID.Substring(0, stringWithoutID.IndexOf(":")-2);
        var score = stringWithoutID.Substring(stringWithoutID.IndexOf(":")+2, stringWithoutID.IndexOf("}")-stringWithoutID.IndexOf(":")-3);
        highScore.Score = Convert.ToInt32(score);

        return highScore;
    }
}


//inline class
public class HighScore
{
    public string UserName{get;set;}
    public int Score{get;set;}
}

