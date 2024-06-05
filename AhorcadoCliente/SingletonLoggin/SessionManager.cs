﻿using AhorcadoCliente.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SessionManager
{
    private static SessionManager _instance;
    private static readonly object _lock = new object();

    public Player LoggedInPlayer { get; private set; }

    private SessionManager() { }

    public static SessionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionManager();
                    }
                }
            }
            return _instance;
        }
    }

    public void Login(Player player)
    {
        LoggedInPlayer = player;
    }

    public void Logout()
    {
        LoggedInPlayer = null;
    }
}
