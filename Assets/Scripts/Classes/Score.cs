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
    int score;
    int time;
    int aerus;
    int exp;
    int venetia;
    bool win;

    public DateTime GetDate()
    {
        return this.date;
    }

    public Map GetMap()
    {
        return this.map;
    }

    public int GetScore()
    {
        return this.score;
    }
    public int GetTime()
    {
        return this.time;
    }
    public int GetAerus()
    {
        return this.aerus;
    }

    public int GetExp()
    {
        return this.exp;
    }
    public int GetVenetia()
    {
        return this.venetia;
    }

    public bool IsWin()
    {
        return this.win;
    }


    public Score(DateTime date, Map map, int score, int time, int aerus, int exp, int venetia, bool win)
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

    public static List<Dictionary<string, object>> ScoresToJson(List<Score> scores)
    {
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        foreach (Score score in scores)
        {
            Dictionary<string, object> dictScore = new Dictionary<string, object>();
            string mapStr = "" + score.map;
            dictScore["date"] = DateTime.Now;
            dictScore["map"] = mapStr[mapStr.Length - 1];
            dictScore["score"] = score.score;
            dictScore["time"] = score.time;
            dictScore["aerus"] = score.aerus;
            dictScore["exp"] = score.exp;
            dictScore["venetia"] = score.venetia;
            dictScore["is_win"] = score.win ? 1 : 0;

            data.Add(dictScore);
        }
        return data;
    }

    public static List<Score> JsonToScores(List<Dictionary<string, object>> data)
    {
        List<Score> scores = new List<Score>();
        foreach (var score in data)
        {
            Map map = (Map)Enum.Parse(typeof(Map), "Stage" + score["map"]);
            scores.Add(
                new Score(
                    (DateTime)score["date"],
                    map,
                    int.Parse(score["score"].ToString()),
                    int.Parse(score["time"].ToString()),
                    int.Parse(score["aerus"].ToString()),
                    int.Parse(score["exp"].ToString()),
                    int.Parse(score["venetia"].ToString()),
                    int.Parse(score["is_win"].ToString()) == 1 ? true : false
                )
            );
        }
        return scores;
    }

    public static Score GetHighScore()
    {
        List<Score> winScores = GameManager.scores
                .Where(scoreObj => scoreObj.IsWin()).ToList();
        if (winScores.Count == 0)
        {
            return null;
        }
        return winScores
                .OrderByDescending(scoreObj => scoreObj.score)
                .ToList()[0];
    }

    public static Score GetHighScoreByMap(Map map)
    {
        List<Score> winScores = GameManager.scores
                .Where(scoreObj => scoreObj.IsWin()).ToList();

        if (winScores.Count == 0)
        {
            return null;
        }

        return winScores
                .Where(scoreObj => scoreObj.GetMap() == map)
                .OrderByDescending(scoreObj => scoreObj.score)
                .ToList()[0];
    }

    public static Score GetBestTime()
    {
        List<Score> winScores = GameManager.scores
                .Where(scoreObj => scoreObj.IsWin()).ToList();
        if (winScores.Count == 0)
        {
            return null;
        }
        return winScores
                .OrderBy(scoreObj => scoreObj.time)
                .ToList()[0];
    }

    public static Score GetBestTimeByMap(Map map)
    {
        List<Score> winScores = GameManager.scores
                .Where(scoreObj => scoreObj.IsWin()).ToList();
        if (winScores.Count == 0)
        {
            return null;
        }
        return winScores
                .Where(scoreObj => scoreObj.GetMap() == map)
                .OrderBy(scoreObj => scoreObj.time)
                .ToList()[0];
    }


}