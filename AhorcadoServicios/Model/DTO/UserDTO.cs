﻿using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using static AhorcadoServicios.Model.POCO.Player;

namespace AhorcadoServicios.Model.DTO
{
    public class UserDTO
    {
        public static Player logIn(string email, string password)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var user = (from us in dataContext.GetTable<Player>()
                            where us.Email == email && us.Password == password
                            select us).FirstOrDefault();
                return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static bool registerUser(Player newPlayer)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContex = new DataContext(connection);
                dataContex.GetTable<Player>().InsertOnSubmit(newPlayer);
                dataContex.SubmitChanges();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static bool emailAlreadyRegistered(string email)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                bool emailRegistered = dataContext.GetTable<Player>().Any(pl => pl.Email == email);
                return emailRegistered;
            }
            catch (SqlException ex)
            {
                throw ex; 
            }
        }

        public static bool telephoneAlreadyRegistered(string telephone)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                bool telephoneRegistered = dataContext.GetTable<Player>().Any(pl => pl.PhoneNumber == telephone);
                return telephoneRegistered;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static bool nicknameAlreadyRegistered(string nickname)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                bool nicknameRegistered = dataContext.GetTable<Player>().Any(pl => pl.NickName == nickname);
                return nicknameRegistered;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}