using SQLite4Unity3d;
using System;
using nsDB;
using UnityEngine;

public class StartPage : MonoBehaviour
{ 
    private SQLiteConnection _db = DB.database.getConn();
    
    private void Start()
    {
        try
        {
            SQLiteConnection db = DB.database.getConn();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
