namespace WY.RMS.Component.Data.EF.Interface
{
    /// <summary>
    ///  业务单元操作接口
    /// </summary>
    public interface IUnitOfWork
    {

        #region 方法

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        int Commit();

        #endregion
    }
}
