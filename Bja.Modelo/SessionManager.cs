﻿using Bja.Entidades;
using Bja.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bja.Modelo
{
    public sealed class SessionManager
    {
        private static Session session = null;

        private static volatile SessionManager instance;
        private static object syncRoot = new Object();

        private SessionManager() { }

        public static SessionManager initSession(User user)
        {
            if (instance == null) 
            {
                //verify lock of object of multiple threads
                lock (syncRoot) 
                {
                    if (instance == null)
                    {
                        instance = new SessionManager();

                        BjaContext context = new BjaContext();

                        session = new Session();
                        session.Id = IdentifierGenerator.NewId();
                        session.IdUser = user.Id;
                        session.UserName = user.UserName;
                        session.CompleteName = user.CompleteName;
                        session.InitDate = DateTime.Now;

                        context.Sessions.Add(session);

                        context.SaveChanges();
                    }
                }
            }

            return instance;
            
        }

        public static Session getCurrentSession()
        {
            return session;
        }

        public static void endSession()
        {
            BjaContext context = new BjaContext();

            session.EndDate = DateTime.Now;

            context.Sessions.Attach(session);

            context.Entry(session).State = System.Data.EntityState.Modified;

            context.SaveChanges();

            instance = null;
        }

        public static long getSessionIdentifier()
        {
            return session.Id;
        }


    }
}
