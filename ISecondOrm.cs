using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    public interface ISecondOrm
    {
        ISecondOrmContext Context { get; }
    }
    public interface ISecondOrmContext
    {
        HashSet<DbUserEntity> Users { get; }
        HashSet<DbUserInfoEntity> UserInfos { get; }
    }
}
