using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace StatisticUser.Logic.Services
{
    [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
    public class DataSourceLoadOptions : DataSourceLoadOptionsBase { }
}
