using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private string dbPath;

    void Start()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/findme.db"; // Path to save the database file.
        CreateDatabase();
    }

    void CreateDatabase()
{
    using (var connection = new SqliteConnection(dbPath))
    {
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            // Create Players table if it doesn't exist, including the coins column
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS players (
                    player_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    username TEXT UNIQUE, 
                    password TEXT, 
                    coins INT DEFAULT 0,
                    total_games_played INT DEFAULT 0,
                    total_wins INT DEFAULT 0,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );";
            command.ExecuteNonQuery();
        }
    }
}

}
