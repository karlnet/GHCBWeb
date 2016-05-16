using GHCBWeb.Data.Entities;
using GHCBWeb.Infrastructure;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GHCBWeb.Data
{
    public class GHCBRepository: IGHCBRepository 
    {
        private ApplicationDbContext db;   //= new ApplicationDbContext();

        public GHCBRepository(ApplicationDbContext ctx)
        {
            db = ctx;
            GetAppId();

        }

        //  Board

        public IQueryable<Board> GetAllBoards(string userId)
        {
            return db.ApplicationUserBoards.Include(x =>x.board ).Where(x => x.ApplicationUserName == userId).Select(o => o.board);
        }
        public Board GetBoardById(int entityId)
        {
            return db.GetByKey<Board>(entityId);
        }
        public Board GetBoardByMac(string mac)
        {
            return db.Where<Board>(x => x.MAC.Equals(mac)).First();
        }
        public bool IsExistsBoard(int entityId)
        {
            return db.IsExists<Board>(x => x.Id == entityId);
        }
        public bool IsExistsBoard(string mac)
        {
            return db.IsExists<Board>(x => x.MAC.Equals(mac));
        }

        public bool Insert(Board entity)
        {
            return db.Insert<Board>(entity);
        }
        public void InsertOrUpdate(Board entity,string mac)
        {
            db.InsertOrUpdate<Board>(entity, (x => x.MAC.Equals(mac)));

        }
        public void Delete(string mac)
        {
            while (IsExistsBoard(mac))
                db.Delete<Board>(x => x.MAC.Equals(mac));
        }
        //public BoardPort GetBoardPortById(int entityId, string userId)
        //{
        //    throw new NotImplementedException();
        //}


        //  ApplicationUserBoard

        public bool Insert(ApplicationUserBoard entity)
        {
            return db.Insert<ApplicationUserBoard>(entity);
        }
      
        public bool? Update(ApplicationUserBoard entity)
        {
            throw new NotImplementedException();
        }
        public bool CheckBoardUser(int entityId, string userId)
        {
            if (IsExistsBoard(entityId))
                return db.IsExists<ApplicationUserBoard>(x => x.Id == entityId && x.ApplicationUserName == userId);
            return false;
        }
        //  BoardPort
        public IQueryable<BoardPort> GetAllBoardPorts(string mac)
        {
            Board b = GetBoardByMac(mac);
            return GetAllBoardPorts(b.Id);

        }
        public IQueryable<BoardPort> GetAllBoardPorts(int entityId)
        {
            return db.BoardPorts.Include(o => o.portDescription).Where(x => x.BoardId == entityId);
        }
        public bool Insert(BoardPort entity)
        {
            return db.Insert<BoardPort>(entity);
        }
        public void DeleteBoardPort(int boardId, int port)
        {
            while (IsExistsBoardPort(boardId, port))
                db.Delete<BoardPort>(x => x.BoardId == boardId && x.Port == port);
        }
        public bool IsExistsBoardPort(int boardId, int port) {
           return db.IsExists<BoardPort>(x => x.BoardId == boardId && x.Port == port);
           
        }

        //  PortDescription
        public IQueryable<PortDescription> GetAllPortDescriptions()
        {
            return db.GetAll<PortDescription>();
        }
        //  APPId
        public void GetAppId()
        {
            App app = db.Where<App>(x => x.Name.Equals("app")).First();
            MyAPPs.AppID=app.AppId;
            MyAPPs.AppSecret = app.AppSecretKey;

            App product = db.Where<App>(x => x.Name.Equals("product")).First();
            MyAPPs.ProductID = product.AppId;
            MyAPPs.ProductSecertKey = product.AppSecretKey;

            App qiniu = db.Where<App>(x => x.Name.Equals("qiniu")).First();
            MyAPPs.QiniuAccesskey = qiniu.AppId;
            MyAPPs.QiniuSecretkey = qiniu.AppSecretKey;
        }

        //public GHCBRepository()
        //{

        //}
        //public void setDbContext(ApplicationDbContext applicationDbContext)
        //{
        //    db = applicationDbContext;

        //}
        //public IQueryable<Board> GetAllBoards()
        //{
        //    return db.GetAll<T>();
        //}

        //public T GetById(int Id)
        //{
        //    return db.GetByKey<T>(Id);
        //}
        //public IQueryable<T> GetByProperty(Expression<Func<T, bool>> predicate)
        //{
        //    return db.Where<T>(predicate);
        //}
        //public  bool IsExists( Expression<Func<T, bool>> predicate)
        //{
        //    return db.IsExists<T>(predicate);
        //}
        ////public IQueryable<Board> GetByProperty(string MAC)
        ////{
        ////    return db.Where<Board>(o => o.MAC.Equals(MAC));
        ////}
        //public bool Insert(T entity)
        //{
        //    return db.Insert<T>(entity);
        //}
        //public  bool? Update(T entity) 
        //{
        //    return db.Update<T>(entity);
        //}
        //public int? DeleteById( params object[] keyValues) 
        //{
        //    return db.DeleteByKey<T>(true,keyValues);
        //}
        //public  void DeleteByProperty( Expression<Func<T, bool>> predicate) 
        //{
        //    db.Delete<T>(predicate);
        //}
        //public IQueryable<BoardPort> GetBoardPorts(int boardId)
        //{
        //    return db.Where<BoardPort>(b => b.BoardId == boardId);
        //}




        //public bool Insert(ApplicationUserBoard applicationUserBoard)
        //{
        //    return  db.Insert<ApplicationUserBoard>(applicationUserBoard);

        //}

        //public bool Insert(BoardPort boardPort) {

        //   return db.Insert<BoardPort>(boardPort);
        //}

        //public bool LoginUser(string userName, string password)
        //{
        //    //var student = db.Students.Where(s => s.UserName == userName).SingleOrDefault();

        //    //if (student != null)
        //    //{
        //    //    if (student.Password == password)
        //    //    {
        //    //        return true;
        //    //    }
        //    //}

        //    return false;
        //}

















    }
}