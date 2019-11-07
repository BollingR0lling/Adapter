using System.Linq;

namespace Adapter
{
    public interface IOrmAdapter // ITarget
    {
        (DbUserEntity, DbUserInfoEntity) Get(int userId);
        void Add(DbUserEntity user, DbUserInfoEntity userInfo);
        void Remove(int userId);
    }

    public class FirstAdapter : IOrmAdapter
    {
        IFirstOrm<DbUserEntity> _firstOrm1;
        IFirstOrm<DbUserInfoEntity> _firstOrm2;
        public FirstAdapter(IFirstOrm<DbUserEntity> _firstOrm1 , IFirstOrm<DbUserInfoEntity> _firstOrm2)
        {
            this._firstOrm1 = _firstOrm1;
            this._firstOrm2 = _firstOrm2;
        }
        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        {
            _firstOrm1.Add(user);
            _firstOrm2.Add(userInfo);
        }
        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            var user = _firstOrm1.Read(userId);
            var userInfo = _firstOrm2.Read(user.InfoId);
            return (user, userInfo);
        }
        public void Remove(int userId)
        {
            var user = _firstOrm1.Read(userId);
            var userInfo = _firstOrm2.Read(user.InfoId);

            _firstOrm2.Delete(userInfo);
            _firstOrm1.Delete(user);
        }
    }

    public class SecondAdapter : IOrmAdapter
    {
        ISecondOrm _secondOrm;
        public SecondAdapter(ISecondOrm _secondOrm)
        {
            this._secondOrm = _secondOrm;
        }
        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        {
            _secondOrm.Context.Users.Add(user);
            _secondOrm.Context.UserInfos.Add(userInfo);
        }
        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            
            var user = _secondOrm.Context.Users.First(i => i.Id == userId);
            var userInfo = _secondOrm.Context.UserInfos.First(i => i.Id == user.InfoId);
            return (user, userInfo);
        }
        public void Remove(int userId)
        {
            var user = _secondOrm.Context.Users.First(i => i.Id == userId);
            var userInfo = _secondOrm.Context.UserInfos.First(i => i.Id == user.InfoId);
            _secondOrm.Context.Users.Remove(user);
            _secondOrm.Context.UserInfos.Remove(userInfo);
        }
    }
}