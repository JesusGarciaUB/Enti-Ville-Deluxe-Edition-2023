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

    public static Database _DATABASE;

    private void Awake()
    {
        if (_DATABASE == null) _DATABASE = this;
        else Destroy(this);

        DontDestroyOnLoad(this);
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

    public void DeleteGame()
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "DELETE FROM plants_users WHERE id_user = 1";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        StarterCrop();
        ResetSave();
        DeleteAllCells();
    }

    private void DeleteAllCells()
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "DELETE FROM savedgames_cells WHERE id_savedgame = 1";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
    }
    private void ResetSave()
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "UPDATE savedgames SET money = 0 WHERE id_user = 1";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
    }

    private void StarterCrop()
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "INSERT INTO plants_users (id_plant, id_user) VALUES (1,1)";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
    }

    public void LoadGame()
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM savedgames WHERE id_user = 1";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Inventory._INVENTORY.ChangeOnMoney(reader.GetDecimal(3));
        }

        LoadCells();
    }

    private void LoadCells()
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM savedgames_cells WHERE id_savedgame = 1";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Crop> crops = CellsBucket._CELLS.GetCrops();

        while (reader.Read())
        {
            int x = reader.GetInt32(1);
            int y = reader.GetInt32(2);
            int id = reader.GetInt32(4);

            foreach (Crop crop in crops)
            {
                Vector2 coord = crop.GetCoords();
                if (coord.x == x && coord.y == y)
                {
                    PlantFromLoad(crop, id);
                }
            }
        }
    }

    private void PlantFromLoad(Crop crop, int id)
    {
        foreach (Plant plant in plants)
        {
            if (plant.Id == id)
            {
                plant.PlantThis(crop.gameObject);
                Inventory._INVENTORY.selectedCrop = plant;
                Inventory._INVENTORY.Planted();
                Inventory._INVENTORY.ReturnToNormal();
                Inventory._INVENTORY.PlantedFromLoad();
                Inventory._INVENTORY.ClickedOutside();
            }
        }
    }

    public void SaveCurrentGame()
    {
        SaveMoney();
        DeleteAllCells();
        SaveCells();
    }

    private void SaveMoney()
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "UPDATE savedgames SET money = "+ Inventory._INVENTORY.GetMoney() +" WHERE id_user = 1";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
    }
    private void SaveCells()
    {
        foreach (Crop crop in CellsBucket._CELLS.GetCrops())
        {
            if (!crop.CanPlant()) SaveCell(crop);
        }
    }

    private void SaveCell(Crop crop)
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "INSERT INTO savedgames_cells (x, y, time, id_plant, id_savedgame) VALUES ("+crop.GetCoords().x+", "+crop.GetCoords().y+", 0, "+crop.GetPlant().GetComponent<PlantGrow>().GetId()+", 1)";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
    }

    public void BuySomething(int id)
    {
        IDbCommand cmnd_read = connection.CreateCommand();
        IDataReader reader;
        string query = "INSERT INTO plants_users (id_plant, id_user) VALUES ("+id+", 1)";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        SaveCurrentGame();
    }
}
