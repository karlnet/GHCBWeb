using GHCBWeb.Data.Entities;
using GHCBWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GHCBWeb.Data
{
    public interface IGHCBRepository //<T> where T : class
    {
        //  Board 

        IQueryable<Board> GetAllBoards(string userId);
        Board GetBoardById(int entityId);
        Board GetBoardByMac(string mac);
        bool IsExistsBoard(int entityId);
        bool IsExistsBoard(string mac);        
        bool Insert(Board entity);
        void InsertOrUpdate(Board entity, string mac);
        void Delete(string mac);



        // ApplicationUserBoard

        bool Insert(ApplicationUserBoard entity);
        bool? Update(ApplicationUserBoard entity);
       
        bool CheckBoardUser(int entityId, string userId);


        //BoardPort  
        IQueryable<BoardPort> GetAllBoardPorts(string mac);
        IQueryable<BoardPort> GetAllBoardPorts(int entityId);
        bool IsExistsBoardPort(int boardId, int port);
        bool Insert(BoardPort entity);
        void DeleteBoardPort(int boardId, int port);

       

        //PortDescription  start
        IQueryable<PortDescription> GetAllPortDescriptions();

        //App
        void GetAppId();

    }
}