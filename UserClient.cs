using System.Linq;

namespace Adapter
{
    public class UserClient
    {
        private IOrmAdapter _ormAdapter;
        private IFirstOrm<DbUserEntity> _firstOrm1;
        private IFirstOrm<DbUserInfoEntity> _firstOrm2;
        private ISecondOrm _secondOrm;
        private bool _useFirstOrm = true;
        /*FirstAdapter firstAdapter = new FirstAdapter();
        SecondAdapter secondAdapter = new SecondAdapter();*/

        public UserClient(IOrmAdapter ormAdapter)
        {
            _ormAdapter = ormAdapter;
        }
        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            return _ormAdapter.Get(userId);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        { 
            _ormAdapter.Add(user, userInfo);
        }

        public void Remove(int userId)
        {
            _ormAdapter.Remove(userId);
        }
    }
}