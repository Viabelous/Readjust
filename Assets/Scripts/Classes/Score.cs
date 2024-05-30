using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class Score
{
    DateTime date;
    Map map;
    float score;
    float time;
    float aerus;
    float exp;
    float venetia;
    bool win;

    public DateTime GetDate()
    {
        return this.date;
    }

    public Map GetMap()
    {
        return this.map;
    }

    public float GetScore()
    {
        return this.score;
    }
    public float GetTime()
    {
        return this.time;
    }
    public float GetAerus()
    {
        return this.aerus;
    }

    public float GetExp()
    {
        return this.exp;
    }
    public float GetVenetia()
    {
        return this.venetia;
    }

    public bool IsWin()
    {
        return this.win;
    }


    public Score(DateTime date, Map map, float score, float time, float aerus, float exp, float venetia, bool win)
    {
        this.date = date;
        this.map = map;
        this.score = score;
        this.time = time;
        this.aerus = aerus;
        this.exp = exp;
        this.venetia = venetia;
        this.win = win;
    }

    public static Dictionary<string, Dictionary<DateTime, List<float>>> ScoresToJson()
    {
        Dictionary<string, Dictionary<DateTime, List<float>>> data = new Dictionary<string, Dictionary<DateTime, List<float>>>();
        foreach (Score score in GameManager.scores)
        {
            Dictionary<DateTime, List<float>> scoreValue = new Dictionary<DateTime, List<float>>();
            scoreValue[score.date] = new List<float>() {
                score.score, score.time, score.aerus, score.exp, score.venetia,score.win ? 1 : 0
            };
            data[score.map.ToString()] = scoreValue;
        }
        return data;
    }

    public static List<Score> JsonToScores(Dictionary<string, Dictionary<DateTime, List<float>>> data)
    {
        List<Score> scores = new List<Score>();
        foreach (var scoreMap in data)
        {
            Map map = (Map)Enum.Parse(typeof(Map), scoreMap.Key);
            foreach (var scoreDate in scoreMap.Value)
            {
                DateTime date = scoreDate.Key;
                Score score = new Score(
                    date,
                    map,
                    scoreDate.Value[0], // score
                    scoreDate.Value[1], // time
                    scoreDate.Value[2], // aerus
                    scoreDate.Value[3], // exp
                    scoreDate.Value[4], // venetia
                    scoreDate.Value[5] == 1 ? true : false
                );
                scores.Add(score);
            }
        }
        return scores;
    }

    public static Score GetHighScore()
    {
        return GameManager.scores
        .Where(scoreObj => scoreObj.IsWin())
        .OrderByDescending(scoreObj => scoreObj.score)
        .ToList()[0];
    }
    public static Score GetHighScoreByMap(Map map)
    {
        return GameManager.scores
        .Where(scoreObj => scoreObj.IsWin() && scoreObj.GetMap() == map)
        .OrderByDescending(scoreObj => scoreObj.score)
        .ToList()[0];
    }

    public static Score GetBestTime()
    {
        return GameManager.scores
        .Where(scoreObj => scoreObj.IsWin())
        .OrderBy(scoreObj => scoreObj.time)
        .ToList()[0];
    }
    public static Score GetBestTimeByMap(Map map)
    {
        return GameManager.scores
        .Where(scoreObj => scoreObj.IsWin() && scoreObj.GetMap() == map)
        .OrderBy(scoreObj => scoreObj.time)
        .ToList()[0];
    }


}