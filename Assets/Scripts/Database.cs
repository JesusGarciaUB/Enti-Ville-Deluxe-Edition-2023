using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using Unity.VisualScripting;

public class Database : MonoBehaviour
{
    private IDbConnection connection;
    private string db_name = "entifarm.db";
    public List<Plant> plants = new List<Plant>();
    [SerializeField] private List<GameObject> plants_prefabs = new List<GameObject>();

    private void Awake()
    {
        connection = new SqliteConnection(string.Format("URI=file:{0}", db_name));
        connection.Open();
        getPlantsDB();
    }

    private List<Plant> getPlantsDB()
    {
        if (plants.Count == 0)
        {
            IDbCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM plants";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                Plant plant = Plant.CreatePlant(reader.GetInt32(0), reader.GetString(1), reader.GetFloat(2), reader.GetInt32(3), reader.GetDecimal(4), reader.GetDecimal(5), plants_prefabs[reader.GetInt32(0) - 1]);
                plants.Add(plant);
            }

        }

        return plants;
    }

    public List<Plant> getPlants()
    {
        return plants;
    }

    public List<KeyValuePair<int, int>> GetInventory()
    {
        List<KeyValuePair<int, int>> inventory = new List<KeyValuePair<int, int>>();
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM plants_users WHERE id_user=1";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            inventory.Add(new KeyValuePair<int, int>(reader.GetInt32(1), reader.GetInt32(2)));
        }

        return inventory;
    }
}
