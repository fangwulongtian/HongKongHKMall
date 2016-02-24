﻿using BrCms.Framework.Collections;
using HKTHMall.Domain.Models;
using HKTHMall.Domain.OfficialWeb.Models.Suppliers;
using HKTHMall.Services.OfficialWebSuppliers.Impl;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKTHMall.Services.OfficialWebSuppliers
{
    public class SuppliersService : BaseService, ISuppliersService
    {
        /// <summary>
        /// 获取供应商列表（wuyf）
        /// </summary>
        /// <param name="parentId">父Id</param>
        /// <returns>供应商列表</returns>
        public ResultModel GetSuppliers(SalesSuppliersModel model)
        {
            var tb = _database.Db.Suppliers;
            var where = new SimpleExpression(1, 1, SimpleExpressionType.Equal); //

            if (model.SupplierId > 0)
            {
                //ID
                where = new SimpleExpression(where, tb.SupplierId == model.SupplierId, SimpleExpressionType.And);
            }

            if (!string.IsNullOrEmpty(model.LinkMan) && model.LinkMan.Trim() != "")
            {
                //联系人
                where = new SimpleExpression(where,
                    tb.LinkMan.Like("%" + model.LinkMan.Trim() + "%"), SimpleExpressionType.And);
            }
            if (!string.IsNullOrEmpty(model.SupplierName) && model.SupplierName.Trim() != "")
            {
                //供应商名称
                where = new SimpleExpression(where,
                    tb.SupplierName.Like("%" + model.SupplierName.Trim() + "%"), SimpleExpressionType.And);
            }
            if (!string.IsNullOrEmpty(model.Telephone) && model.Telephone.Trim() != "")
            {
                //电话
                where = new SimpleExpression(where,
                    tb.Telephone.Like("%" + model.Telephone.Trim() + "%"), SimpleExpressionType.And);
            }
            if (!string.IsNullOrEmpty(model.Mobile) && model.Mobile.Trim() != "")
            {
                //手机
                where = new SimpleExpression(where,
                    tb.Mobile.Like("%" + model.Mobile.Trim() + "%"), SimpleExpressionType.And);
            }
            

            dynamic one, pc;
            dynamic two;

            var query = tb
                .Query()
                .LeftJoin(_database.Db.THArea.As("t1"), out one).On(one.THAreaID == tb.THAreaID)
                .LeftJoin(_database.Db.THArea.As("t2"), out two).On(two.THAreaID == one.ParentID)
                .LeftJoin(_database.Db.THArea_lang.As("t3"), out pc).On(pc.THAreaID == tb.THAreaID && pc.LanguageID == model.Lang)
                
                .Select(
                    tb.SupplierId,
                    tb.SupplierName,
                    tb.THAreaID,
                    tb.Address,
                    tb.LinkMan,
                    tb.Telephone,
                    tb.Mobile,
                    tb.Remark,
                    tb.CreateBy,
                    tb.CreateDT,
                    tb.UpdateBy,
                    tb.UpdateDT,
                    //tb.UserName,
                    tb.PassWord,

                    pc.AreaName,
                    one.ParentID.As("ShiTHAreaID"),
                    two.ParentID.As("ShengTHAreaID")
                )
                .Where(where)
                .OrderByCreateDTDescending();

            var result = new ResultModel
            {
                Data = new SimpleDataPagedList<SuppliersModel>(query,
                    model.PagedIndex, model.PagedSize)
            };

            return result;
        }

        /// <summary>
        /// 根据手机号码查询供应商信息（wuyf）,登录用了
        /// </summary>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public ResultModel GetSuppliersMobile(string Mobile, string PassWord)
        {
            var result = new ResultModel
            {
                Data = _database.Db.Suppliers.Find(_database.Db.Suppliers.Mobile == Mobile && _database.Db.Suppliers.PassWord == PassWord)

            };
            return result;
        }
    }
}
