using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
/// <summary>
/// Descripción breve de Conexion
/// </summary>
public class Conexion
{
    public Conexion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
       

    }
    public MySqlConnectionStringBuilder Conex()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = "localhost";
        builder.UserID = "root";
        builder.Password = "1234";
        builder.Database = "bdLicitacion";
        return builder;

    }
}