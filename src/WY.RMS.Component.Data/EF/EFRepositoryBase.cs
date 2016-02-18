/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/9/6 15:45:22  
*************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using WY.RMS.Component.Data.EF.Interface;
using WY.RMS.Component.Tools;
using WY.RMS.Component.Tools.helpers;

namespace WY.RMS.Component.Data.EF
{
    public abstract class EFRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        #region 属性字段

        /// <summary>
        ///     获取 仓储上下文的实例
        /// </summary>
        protected EFDbContext Context = EFContextFactory.GetCurrentDbContext();

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get { return Context.Set<TEntity>(); }
        }
        /// <summary>
        /// (贪婪加载)返回指定实体数据集
        /// </summary>
        /// <param name="IncludeList">贪婪加载属性集合</param>
        /// <returns>指定实体数据集</returns>
        public virtual IQueryable<TEntity> GetEntitiesByEager(IEnumerable<string> IncludeList)
        {
            IQueryable<TEntity> dbset = Context.Set<TEntity>();
            foreach (var item in IncludeList)
            {
                dbset = dbset.Include<TEntity>(item);
            }
            return dbset;
        }

        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            return isSave ? Context.SaveChanges() : 0;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    EntityState state = Context.Entry(entity).State;
                    if (state == EntityState.Detached)
                    {
                        Context.Entry(entity).State = EntityState.Added;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
            return isSave ? Context.SaveChanges() : 0;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TKey id, bool isSave = true)
        {
            PublicHelper.CheckArgument(id, "id");
            TEntity entity = Context.Set<TEntity>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            Context.Entry(entity).State = EntityState.Deleted;
            return isSave ? Context.SaveChanges() : 0;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    Context.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
            return isSave ? Context.SaveChanges() : 0;
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            PublicHelper.CheckArgument(predicate, "predicate");
            //List<TEntity> entities = Context.Set<TEntity>().Where(predicate).ToList();
            //return entities.Count > 0 ? Delete(entities, isSave) : 0;
            Context.Set<TEntity>().Where(predicate).Delete();
            return isSave ? Context.SaveChanges() : 0;
        }

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Update(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            Context.Update<TEntity, TKey>(entity);
            return isSave ? Context.SaveChanges() : 0;
        }

        /// <summary>
        /// 使用附带新值的实体信息更新指定实体属性的值
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="isSave">是否执行保存</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        /// <returns>操作影响的行数</returns>
        public int Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, bool isSave = true)
        {
            //throw new NotSupportedException("上下文公用，不支持按需更新功能。");
            PublicHelper.CheckArgument(propertyExpression, "propertyExpression");
            PublicHelper.CheckArgument(entity, "entity");
            Context.Update<TEntity, TKey>(propertyExpression, entity);
            if (isSave)
            {
                var dbSet = Context.Set<TEntity>();
                dbSet.Local.Clear();
                var entry = Context.Entry(entity);
                return Context.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(TKey key)
        {
            PublicHelper.CheckArgument(key, "key");
            return Context.Set<TEntity>().Find(key);
        }

        #endregion

    }
}
