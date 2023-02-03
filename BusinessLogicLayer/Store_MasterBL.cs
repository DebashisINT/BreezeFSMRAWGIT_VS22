/*************************************************************************************************************
Rev 1.0     Sanchita    V2.0.38     20/01/2023      Need to increase the length of the Description field of Product Master. Refer: 25603    
******************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class Store_MasterBL
    {
        #region Market
        public DataTable GetMarketList()
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_sMarketList"))
                {
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int InsertsMarket(string MarketCode, int Country, int State, int City, string MarketName, string MarketDescription,
            string MarketAddress, string MarketEmail, string MarketPhoneNo, string MarketWebsite, string MarketContactPerson, int CreatedUser)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertsMarket"))
                {
                    proc.AddVarcharPara("@MarketCode", 100, MarketCode);
                    proc.AddIntegerPara("@Country", Country);
                    proc.AddIntegerPara("@State", State);
                    proc.AddIntegerPara("@City", City);
                    proc.AddVarcharPara("@MarketName", 100, MarketName);
                    proc.AddVarcharPara("@MarketDescription", 300, MarketDescription);
                    proc.AddVarcharPara("@MarketAddress", 100, MarketAddress);
                    proc.AddVarcharPara("@MarketEmail", 100, MarketEmail);
                    proc.AddVarcharPara("@MarketPhoneNo", 20, MarketPhoneNo);
                    proc.AddVarcharPara("@MarketWebsite", 100, MarketWebsite);
                    proc.AddVarcharPara("@MarketContactPerson", 300, MarketContactPerson);
                    proc.AddIntegerPara("@CreatedUser", CreatedUser);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int UpdatesMarket(int MarketId, string MarketCode, int Country, int State, int City, string MarketName, string MarketDescription,
            string MarketAddress, string MarketEmail, string MarketPhoneNo, string MarketWebsite, string MarketContactPerson, int ModifyUser)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("UpdatesMarket"))
                {
                    proc.AddIntegerPara("@MarketId", MarketId);
                    proc.AddVarcharPara("@MarketCode", 100, MarketCode);
                    proc.AddIntegerPara("@Country", Country);
                    proc.AddIntegerPara("@State", State);
                    proc.AddIntegerPara("@City", City);
                    proc.AddVarcharPara("@MarketName", 100, MarketName);
                    proc.AddVarcharPara("@MarketDescription", 500, MarketDescription);
                    proc.AddVarcharPara("@MarketAddress", 100, MarketAddress);
                    proc.AddVarcharPara("@MarketEmail", 100, MarketEmail);
                    proc.AddVarcharPara("@MarketPhoneNo", 20, MarketPhoneNo);
                    proc.AddVarcharPara("@MarketWebsite", 100, MarketWebsite);
                    proc.AddVarcharPara("@MarketContactPerson", 100, MarketContactPerson);
                    proc.AddIntegerPara("@ModifyUser", ModifyUser);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int InsertsMarketLog(int MarketId)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertsMarketLog"))
                {
                    proc.AddIntegerPara("@MarketId", MarketId);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetMarketListById(int MarketId)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_sMarketListById"))
                {
                    proc.AddIntegerPara("@MarketId", MarketId);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }
        #endregion

        #region Product
        public DataTable GetsProductList()
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_sProductList"))
                {
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int InsertProduct(string ProductCode, string ProductName, string ProductDescription, string ProductType, int? ProductClassCode,
              string ProductGlobalCode, int? ProductTradingLot, int? productTradingLotUnit, int? ProductQuoteCurrency, int? ProductQuoteLot,
              int? productQuoteLotUnit, int? ProductDeliveryLot, int? ProductDeliveryLotUnit, int? ProductColor, int? ProductSize, int? ProductCreateUser, Boolean sizeapplicable, Boolean colorapplicable, int? BarCodeSymbology, String BarCode, Boolean isInventory, string stkValuation
              , decimal salePrice, decimal minSalePrice, decimal purPrice, decimal MRP, int? stockUOM, decimal minLvl, decimal reOrderlvl,
              string negativeStock, int? taxCodeSaleScheme, int? taxCodePur, int? taxScheme, Boolean autoApply, string ImagePath, string ProdComponent, string ProdStatus, string hsnValue, int serviceTax,
              decimal quantity, decimal packing, int packingUOM, Boolean isInstall, int Brand, Boolean isCapitalGoods, int TdsCode, string FinYear, Boolean isOldUnit,
              string salesInvMainAct, string salesRetMainAct, string purInv, string purRetMainAct, Boolean FurtheranceToBusiness, Boolean IsServiceItem, decimal reorder_qty
                // Mantis Issue 24299
                , string ProductColorNew = "", string ProductSizeNew = "", string ProductGenderNew = ""
                // End of Mantis Issue 24299
             // Mantis Issue 25469, 25470
             , decimal Discount=0
             // End of Mantis Issue 25469, 25470
            )
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertsProduct"))
                {
                    proc.AddVarcharPara("@ProductCode", 80, ProductCode);
                    proc.AddVarcharPara("@ProductName", 100, ProductName);
                    // Rev 1.0
                    //proc.AddVarcharPara("@ProductDescription", 500, ProductDescription);
                    proc.AddVarcharPara("@ProductDescription", -1, ProductDescription);
                    // End of Rev 1.0
                    proc.AddVarcharPara("@ProductType", 10, ProductType);
                    if (ProductClassCode != null)
                    {
                        proc.AddIntegerPara("@ProductClassCode", ProductClassCode, QueryParameterDirection.Input);
                    }
                    proc.AddVarcharPara("@ProductGlobalCode", 100, ProductGlobalCode);
                    if (ProductTradingLot != null)
                    {
                        proc.AddIntegerPara("@ProductTradingLot", ProductTradingLot, QueryParameterDirection.Input);
                    }
                    if (productTradingLotUnit != null)
                    {
                        proc.AddIntegerPara("@productTradingLotUnit", productTradingLotUnit, QueryParameterDirection.Input);
                    }
                    if (ProductQuoteCurrency != null)
                    {
                        proc.AddIntegerPara("@ProductQuoteCurrency", ProductQuoteCurrency, QueryParameterDirection.Input);
                    }
                    if (ProductQuoteLot != null)
                    {
                        proc.AddIntegerPara("@ProductQuoteLot", ProductQuoteLot, QueryParameterDirection.Input);
                    }
                    if (productQuoteLotUnit != null)
                    {
                        proc.AddIntegerPara("@productQuoteLotUnit", productQuoteLotUnit, QueryParameterDirection.Input);
                    }
                    if (ProductDeliveryLot != null)
                    {
                        proc.AddIntegerPara("@ProductDeliveryLot", ProductDeliveryLot, QueryParameterDirection.Input);
                    }
                    if (ProductDeliveryLotUnit != null)
                    {
                        proc.AddIntegerPara("@ProductDeliveryLotUnit", ProductDeliveryLotUnit, QueryParameterDirection.Input);
                    }
                    //if (ProductColor != null)
                    //{
                    //    proc.AddIntegerPara("@ProductColor", ProductColor, QueryParameterDirection.Input);
                    //}
                    //if (ProductSize != null)
                    //{
                    //    proc.AddIntegerPara("@ProductSize", ProductSize, QueryParameterDirection.Input);
                    //}
                    if (ProductColor != 0)
                    {
                        proc.AddIntegerPara("@ProductColor", ProductColor, QueryParameterDirection.Input);
                    }
                    if (ProductSize != 0)
                    {
                        proc.AddIntegerPara("@ProductSize", ProductSize, QueryParameterDirection.Input);
                    }
                    if (ProductCreateUser != null)
                    {
                        proc.AddIntegerPara("@ProductCreateUser", ProductCreateUser, QueryParameterDirection.Input);
                    }

                    // Mantis Issue 24299
                    if (ProductColorNew != null)
                    {
                        proc.AddVarcharPara("@ProductColorNew",-1, ProductColorNew, QueryParameterDirection.Input);
                    }
                    if (ProductSizeNew != null)
                    {
                        proc.AddVarcharPara("@ProductSizeNew",-1, ProductSizeNew, QueryParameterDirection.Input);
                    }
                    if (ProductGenderNew != null)
                    {
                        proc.AddVarcharPara("@ProductGenderNew",-1, ProductGenderNew, QueryParameterDirection.Input);
                    }
                    // End of Mantis Issue 24299

                    //.................Code Added By Sam on 25102016..........................................................
                    proc.AddBooleanPara("@sProducts_SizeApplicable", sizeapplicable, QueryParameterDirection.Input);
                    proc.AddBooleanPara("@sProducts_ColorApplicable", colorapplicable, QueryParameterDirection.Input);
                    //.................Code Above Added By Sam on 25102016....................................................

                    //Code Added by Debjyoti 30-12-2016
                    proc.AddIntegerPara("@ProductBarCodeType", BarCodeSymbology, QueryParameterDirection.Input);
                    proc.AddVarcharPara("@sProducts_barCode", 50, BarCode);
                    //End here 30-12-2016

                    //Code Added by Debjyoti 04-01-2017
                    proc.AddBooleanPara("@sProductsIsInventory", isInventory, QueryParameterDirection.Input);
                    proc.AddVarcharPara("@sProduct_Stockvaluation", 5, stkValuation);
                    proc.AddDecimalPara("@sProduct_SalePrice", 5, 18, salePrice);
                    proc.AddDecimalPara("@sProduct_MinSalePrice", 5, 18, minSalePrice);
                    proc.AddDecimalPara("@sProduct_PurPrice", 5, 18, purPrice);
                    proc.AddDecimalPara("@sProduct_MRP", 5, 18, MRP);
                    // Mantis Issue 25469, 25470
                    proc.AddDecimalPara("@sProducts_Discount", 5, 18, Discount);
                    // End of Mantis Issue 25469, 25470
                    if (stockUOM != null)
                    {
                        proc.AddIntegerPara("@sProduct_StockUOM", stockUOM, QueryParameterDirection.Input);
                    }
                    proc.AddDecimalPara("@sProduct_MinLvl", 0, 18, minLvl);
                    proc.AddDecimalPara("@sProduct_reOrderLvl", 0, 18, reOrderlvl);
                    proc.AddVarcharPara("@sProduct_NegativeStock", 5, negativeStock);
                    proc.AddIntegerPara("@sProduct_TaxSchemeSale", taxCodeSaleScheme, QueryParameterDirection.Input);
                    proc.AddIntegerPara("@sProduct_TaxSchemePur", taxCodePur, QueryParameterDirection.Input);
                    proc.AddIntegerPara("@sProduct_TaxScheme", taxScheme, QueryParameterDirection.Input);
                    proc.AddBooleanPara("@sProduct_AutoApply", autoApply, QueryParameterDirection.Input);
                    proc.AddVarcharPara("@sProduct_ImagePath", 200, ImagePath);
                    proc.AddVarcharPara("@ProdComponent", 1000, ProdComponent);
                    proc.AddVarcharPara("@sProduct_Status", 5, ProdStatus);
                    //End here 04-01-2017
                    proc.AddVarcharPara("@sProducts_HsnCode", 10, hsnValue);

                    //02-02-2017
                    proc.AddIntegerPara("@sProducts_serviceTax", serviceTax, QueryParameterDirection.Input);

                    //For Packing Details
                    proc.AddDecimalPara("@sProduct_quantity", 5, 18, quantity);
                    proc.AddDecimalPara("@packing_quantity", 5, 18, packing);
                    proc.AddIntegerPara("@packing_saleUOM", packingUOM, QueryParameterDirection.Input);
                    //Packing work end here
                    proc.AddVarcharPara("@ReturnValue", 50, "0", QueryParameterDirection.Output);
                    proc.AddBooleanPara("@sProducts_isInstall", isInstall);
                    proc.AddBooleanPara("@BusinessFurtherness", FurtheranceToBusiness);//Subhabrata
                    proc.AddIntegerPara("@sProducts_Brand", Brand, QueryParameterDirection.Input);
                    proc.AddBooleanPara("@sProducts_isCapitalGoods", isCapitalGoods);
                    proc.AddIntegerPara("@sProducts_tdsCode", TdsCode);
                    proc.AddVarcharPara("@Finyear", 20, FinYear);
                    proc.AddBooleanPara("@sProducts_IsOldUnit", isOldUnit);
                    proc.AddVarcharPara("@sInv_MainAccount", 50, salesInvMainAct);
                    proc.AddVarcharPara("@sRet_MainAccount", 50, salesRetMainAct);
                    proc.AddVarcharPara("@pInv_MainAccount", 50, purInv);
                    proc.AddVarcharPara("@pRet_MainAccount", 50, purRetMainAct);
                    proc.AddBooleanPara("@sProducts_IsServiceItem", IsServiceItem);
                    proc.AddDecimalPara("@reorder_qty", 4, 18, reorder_qty);
                    proc.RunActionQuery();
                    int NoOfRowEffected = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }
        public int InsertStockWithNewProduct(int ProductId, string Finyear)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertStockWithNewProduct"))
                {
                    proc.AddIntegerPara("@ProductId", ProductId);
                    proc.AddVarcharPara("@Finyear", 20, Finyear);

                    proc.RunActionQuery();
                    int NoOfRowEffected = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }
        public int UpdateProduct(int ProductId, string ProductCode, string ProductName, string ProductDescription, string ProductType, int? ProductClassCode,
           string ProductGlobalCode, int? ProductTradingLot, int? productTradingLotUnit, int? ProductQuoteCurrency, int? ProductQuoteLot,
           int? productQuoteLotUnit, int? ProductDeliveryLot, int? ProductDeliveryLotUnit, int? ProductColor, int? ProductSize, int? ModifyUser, Boolean sizeapplicable, Boolean colorapplicable, int? BarCodeSymbology, string BarCode,
           Boolean isInventory, string stkValuation, decimal salePrice, decimal minSalePrice, decimal purPrice, decimal MRP, int? stockUOM, decimal minLvl, decimal reOrdrLvl,
           string negativeStock, int taxCodeSale, int? taxCodePur, int? taxScheme, Boolean autoApply, string ImagePath, string ProdComponent, string prodStatus, string hsnCode, int serviceTax,
           decimal quantity, decimal packing, int packingUOM, bool isInstall, int Brand, Boolean isCapitalGoods, int tdsCode, Boolean sProducts_IsOldUnit,
            string salesInvMainAct, string salesRetMainAct, string purInv, string purRetMainAct, Boolean FurtheranceToBusiness, Boolean IsServiceItem, decimal reorder_qty
            // Mantis Issue 24299
            , string ProductColorNew="", string ProductSizeNew="", string ProductGenderNew=""
            // End of Mantis Issue 24299
            // Mantis Issue 25469, 25470
            , decimal Discount=0
            // End of Mantis Issue 25469, 25470
            )
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_UpdatesProduct"))
                {
                    proc.AddIntegerPara("@ProductId", ProductId);
                    proc.AddVarcharPara("@ProductCode", 100, ProductCode);
                    proc.AddVarcharPara("@ProductName", 100, ProductName);
                    // Rev 1.0
                    //proc.AddVarcharPara("@ProductDescription", 500, ProductDescription);
                    proc.AddVarcharPara("@ProductDescription", -1, ProductDescription);
                    // End of Rev 1.0
                    proc.AddVarcharPara("@ProductType", 10, ProductType);
                    if (ProductClassCode != null)
                    {
                        proc.AddIntegerPara("@ProductClassCode", ProductClassCode, QueryParameterDirection.Input);
                    }
                    proc.AddVarcharPara("@ProductGlobalCode", 100, ProductGlobalCode);
                    if (ProductTradingLot != null)
                    {
                        proc.AddIntegerPara("@ProductTradingLot", ProductTradingLot, QueryParameterDirection.Input);
                    }
                    if (productTradingLotUnit != null)
                    {
                        proc.AddIntegerPara("@productTradingLotUnit", productTradingLotUnit, QueryParameterDirection.Input);
                    }
                    if (ProductQuoteCurrency != null)
                    {
                        proc.AddIntegerPara("@ProductQuoteCurrency", ProductQuoteCurrency, QueryParameterDirection.Input);
                    }
                    if (ProductQuoteLot != null)
                    {
                        proc.AddIntegerPara("@ProductQuoteLot", ProductQuoteLot, QueryParameterDirection.Input);
                    }
                    if (productQuoteLotUnit != null)
                    {
                        proc.AddIntegerPara("@productQuoteLotUnit", productQuoteLotUnit, QueryParameterDirection.Input);
                    }
                    if (ProductDeliveryLot != null)
                    {
                        proc.AddIntegerPara("@ProductDeliveryLot", ProductDeliveryLot, QueryParameterDirection.Input);
                    }
                    if (ProductDeliveryLotUnit != null)
                    {
                        proc.AddIntegerPara("@ProductDeliveryLotUnit", ProductDeliveryLotUnit, QueryParameterDirection.Input);
                    }
                    //if (ProductColor != null)
                    //{
                    //    proc.AddIntegerPara("@ProductColor", ProductColor, QueryParameterDirection.Input);
                    //}
                    //if (ProductSize != null)
                    //{
                    //    proc.AddIntegerPara("@ProductSize", ProductSize, QueryParameterDirection.Input);
                    //}
                    if (ProductColor != 0)
                    {
                        proc.AddIntegerPara("@ProductColor", ProductColor, QueryParameterDirection.Input);
                    }
                    if (ProductSize != 0)
                    {
                        proc.AddIntegerPara("@ProductSize", ProductSize, QueryParameterDirection.Input);
                    }
                    if (ModifyUser != null)
                    {
                        proc.AddIntegerPara("@ModifyUser", ModifyUser, QueryParameterDirection.Input);
                    }
                    // Mantis Issue 24299
                    if (ProductColorNew != null)
                    {
                        proc.AddVarcharPara("@ProductColorNew",-1, ProductColorNew, QueryParameterDirection.Input);
                    }
                    if (ProductSizeNew != null)
                    {
                        proc.AddVarcharPara("@ProductSizeNew",-1, ProductSizeNew, QueryParameterDirection.Input);
                    }
                    if (ProductGenderNew != null)
                    {
                        proc.AddVarcharPara("@ProductGenderNew",-1, ProductGenderNew, QueryParameterDirection.Input);
                    }
                    // End of Mantis Issue 24299
                    //.................Code Added By Sam on 25102016..........................................................
                    proc.AddBooleanPara("@sProducts_SizeApplicable", sizeapplicable, QueryParameterDirection.Input);
                    proc.AddBooleanPara("@sProducts_ColorApplicable", colorapplicable, QueryParameterDirection.Input);
                    //.................Code Above Added By Sam on 25102016....................................................

                    //.................Code Added By Debjyoti on 30122016..........................................................
                    proc.AddIntegerPara("@ProductBarCodeType", BarCodeSymbology, QueryParameterDirection.Input);
                    proc.AddVarcharPara("@sProducts_barCode", 50, BarCode);

                    //-----------------Code added by debjyoti 04-01-2017
                    proc.AddBooleanPara("@sProducts_isInventory", isInventory, QueryParameterDirection.Input);
                    proc.AddVarcharPara("@sProduct_Stockvaluation", 5, stkValuation);
                    proc.AddDecimalPara("@sProduct_SalePrice", 0, 18, salePrice);
                    proc.AddDecimalPara("@sProduct_MinSalePrice", 0, 18, minSalePrice);
                    proc.AddDecimalPara("@sProduct_PurPrice", 0, 18, purPrice);
                    proc.AddDecimalPara("@sProduct_MRP", 0, 18, MRP);
                    // Mantis Issue 25469, 25470
                    proc.AddDecimalPara("@sProducts_Discount", 0, 18, Discount);
                    // End of Mantis Issue 25469, 25470
                    if (stockUOM != null)
                    {
                        proc.AddIntegerPara("@sProduct_StockUOM", stockUOM, QueryParameterDirection.Input);
                    }
                    proc.AddDecimalPara("@sProduct_MinLvl", 0, 18, minLvl);
                    proc.AddDecimalPara("@sProduct_reOrderLvl", 0, 18, reOrdrLvl);
                    proc.AddVarcharPara("@sProduct_NegativeStock", 5, negativeStock);
                    proc.AddIntegerPara("@sProduct_TaxSchemeSale", taxCodeSale, QueryParameterDirection.Input);
                    proc.AddIntegerPara("@sProduct_TaxSchemePur", taxCodePur, QueryParameterDirection.Input);
                    proc.AddIntegerPara("@sProduct_TaxScheme", taxScheme, QueryParameterDirection.Input);
                    proc.AddBooleanPara("@sProduct_AutoApply", autoApply, QueryParameterDirection.Input);
                    proc.AddVarcharPara("@sProduct_ImagePath", 200, ImagePath);
                    proc.AddVarcharPara("@ProdComponent", 1000, ProdComponent);
                    proc.AddVarcharPara("@sProduct_Status", 5, prodStatus);
                    proc.AddVarcharPara("@sProducts_HsnCode", 10, hsnCode);
                    //02-02-2017
                    proc.AddIntegerPara("@sProducts_serviceTax", serviceTax, QueryParameterDirection.Input);

                    //For Packing Details
                    proc.AddDecimalPara("@sProduct_quantity", 5, 18, quantity);
                    proc.AddDecimalPara("@packing_quantity", 5, 18, packing);
                    proc.AddIntegerPara("@packing_saleUOM", packingUOM, QueryParameterDirection.Input);
                    //Packing work end here
                    proc.AddBooleanPara("@sProducts_isInstall", isInstall);
                    proc.AddIntegerPara("@sProducts_Brand", Brand, QueryParameterDirection.Input);
                    proc.AddBooleanPara("@sProducts_isCapitalGoods", isCapitalGoods);

                    proc.AddIntegerPara("@sProducts_tdsCode", tdsCode);
                    proc.AddBooleanPara("@sProducts_IsOldUnit", sProducts_IsOldUnit);

                    proc.AddBooleanPara("@BusinessFurtherness", FurtheranceToBusiness);//Subhabrata

                    proc.AddVarcharPara("@sInv_MainAccount", 50, salesInvMainAct);
                    proc.AddVarcharPara("@sRet_MainAccount", 50, salesRetMainAct);
                    proc.AddVarcharPara("@pInv_MainAccount", 50, purInv);
                    proc.AddVarcharPara("@pRet_MainAccount", 50, purRetMainAct);
                    proc.AddBooleanPara("@sProducts_IsServiceItem", IsServiceItem);
                    proc.AddDecimalPara("@reorder_qty", 4, 18, reorder_qty);

                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetProductDetails(int ProductId)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_sProductDetailsById"))
                {
                    proc.AddIntegerPara("@ProductId", ProductId);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }
        #endregion

        #region Size
        public DataTable GetSizeList()
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_SizeList"))
                {
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int InsertSize(string SizeName, string SizeDescription, int CreatedUser)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertSize"))
                {
                    proc.AddVarcharPara("@SizeName", 100, SizeName);
                    proc.AddVarcharPara("@SizeDescription", 100, SizeDescription);
                    proc.AddIntegerPara("@CreatedUser", CreatedUser);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int UpdateSize(int SizeId, string SizeName, string SizeDescription, int ModifiedUser)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_UpdateSize"))
                {
                    proc.AddIntegerPara("@SizeId", SizeId);
                    proc.AddVarcharPara("@SizeName", 100, SizeName);
                    proc.AddVarcharPara("@SizeDescription", 100, SizeDescription);
                    proc.AddIntegerPara("@ModifiedUser", ModifiedUser);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetSizeDetails(int SizeId)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_SizeDetailsById"))
                {
                    proc.AddIntegerPara("@SizeId", SizeId);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetSizeUOMMapDetails(int SizeDetailsId)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_SizeUOMMapDetailsById"))
                {
                    proc.AddIntegerPara("@SizeDetailsId", SizeDetailsId);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int InsertSizeDetails(int SizeDetailsMainId, string SizeDetailsAttributeName, string SizeDetailsValue, int SizeDetailsUOM, int CreatedUser)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertSizeDetails"))
                {
                    proc.AddIntegerPara("@SizeDetailsMainId", SizeDetailsMainId);
                    proc.AddVarcharPara("@SizeDetailsAttributeName", 100, SizeDetailsAttributeName);
                    proc.AddVarcharPara("@SizeDetailsValue", 100, SizeDetailsValue);
                    proc.AddIntegerPara("@SizeDetailsUOM", SizeDetailsUOM);
                    proc.AddIntegerPara("@CreatedUser", CreatedUser);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int UpdateSizeDetails(int SizeDetailsId, string SizeDetailsAttributeName, string SizeDetailsValue, int SizeDetailsUOM, int ModifiedUser)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_UpdatetSizeDetails"))
                {
                    proc.AddIntegerPara("@SizeDetailsId", SizeDetailsId);
                    proc.AddVarcharPara("@SizeDetailsAttributeName", 100, SizeDetailsAttributeName);
                    proc.AddVarcharPara("@SizeDetailsValue", 100, SizeDetailsValue);
                    proc.AddIntegerPara("@SizeDetailsUOM", SizeDetailsUOM);
                    proc.AddIntegerPara("@ModifiedUser", ModifiedUser);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetSizeDetailsList(int SizeDetailsId)
        {

            {
                ProcedureExecute proc;
                DataTable rtrnvalue;
                try
                {
                    using (proc = new ProcedureExecute("Sp_SizeDetailsListById"))
                    {
                        proc.AddIntegerPara("@SizeDetailsId", SizeDetailsId);
                        rtrnvalue = proc.GetTable();
                        return rtrnvalue;
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    proc = null;
                }
            }
        }
        #endregion

        #region Order
        public int InsertOrder(string OrderCompany, int OrderBranch, string OrderDate, string OrderFinYear, string OrderType, string OrderNo,
            string OrderContactId, string OrderRefNumber, string OrderAgentId, string OrderInstruction, string OrderPaymentTerm, string OrderPaymentDate,
            string OrderDeliveryDate, string OrderDeliveryAt, int OrderDeliveryBranch, int OrderDeliveryWareHouse, int OrderDeliveryAddress,
            string OrderDeliveryOther, int CreatedUser, int OrderPaymentDays, string ParentOrderNo)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("SP_InsertpOrder"))
                {
                    proc.AddVarcharPara("@OrderCompany", 100, OrderCompany);
                    proc.AddIntegerPara("@OrderBranch", OrderBranch);
                    proc.AddVarcharPara("@OrderDate", 50, OrderDate);
                    proc.AddVarcharPara("@OrderFinYear", 20, OrderFinYear);
                    proc.AddVarcharPara("@OrderType", 20, OrderType);
                    proc.AddVarcharPara("@OrderNo", 50, OrderNo);
                    proc.AddVarcharPara("@OrderContactId", 20, OrderContactId);
                    proc.AddVarcharPara("@OrderRefNumber", 20, OrderRefNumber);
                    proc.AddVarcharPara("@OrderAgentId", 50, OrderAgentId);
                    proc.AddVarcharPara("@OrderInstruction", 50, OrderInstruction);
                    proc.AddVarcharPara("@OrderPaymentTerm", 20, OrderPaymentTerm);
                    proc.AddVarcharPara("@OrderPaymentDate", 20, OrderPaymentDate);
                    proc.AddVarcharPara("@OrderDeliveryDate", 20, OrderDeliveryDate);
                    proc.AddVarcharPara("@OrderDeliveryAt", 20, OrderDeliveryAt);
                    proc.AddIntegerPara("@OrderDeliveryBranch", OrderDeliveryBranch);
                    proc.AddIntegerPara("@OrderDeliveryWareHouse", OrderDeliveryWareHouse);
                    proc.AddIntegerPara("@OrderDeliveryAddress", OrderDeliveryAddress);
                    proc.AddVarcharPara("@OrderDeliveryOther", 20, OrderDeliveryOther);
                    proc.AddIntegerPara("@CreatedUser", CreatedUser);
                    proc.AddIntegerPara("@OrderPaymentDays", OrderPaymentDays);
                    proc.AddVarcharPara("@pOrder_ParentOrderNo", 20, ParentOrderNo);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int InsertOrderDetails(int OrderDetailsOrderId, int OrderDetailsProductId, string OrderDetailsDrand, int OrderDetailsSize,
            int OrderDetailsColor, int OrderDetaisBestBeforeMonth, int OrderDetailsBestBeforeYear, int OrderDetailsQuoteCurrency, string OrderDetailsUnitPrice,
            int OrderDetailsPriceLot, string OrderDetailsQuantity, int OrderDetailsQuantityUnit, int OrderDetailsPriceLotUnit, string OrderDetailsRemarks,
            int CreatedUser, string OrderDetailsProductDescription)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertOrderDetails"))
                {
                    proc.AddIntegerPara("@OrderDetailsOrderId", OrderDetailsOrderId);
                    proc.AddIntegerPara("@OrderDetailsProductId", OrderDetailsProductId);
                    proc.AddVarcharPara("@OrderDetailsDrand", 100, OrderDetailsDrand);
                    proc.AddIntegerPara("@OrderDetailsSize", OrderDetailsSize);
                    proc.AddIntegerPara("@OrderDetailsColor", OrderDetailsColor);
                    proc.AddIntegerPara("@OrderDetaisBestBeforeMonth", OrderDetaisBestBeforeMonth);
                    proc.AddIntegerPara("@OrderDetailsBestBeforeYear", OrderDetailsBestBeforeYear);
                    proc.AddIntegerPara("@OrderDetailsQuoteCurrency", OrderDetailsQuoteCurrency);
                    proc.AddVarcharPara("OrderDetailsUnitPrice", 50, OrderDetailsUnitPrice);
                    proc.AddIntegerPara("@OrderDetailsPriceLot", OrderDetailsPriceLot);
                    proc.AddVarcharPara("@OrderDetailsQuantity", 50, OrderDetailsQuantity);
                    proc.AddIntegerPara("@OrderDetailsQuantityUnit", OrderDetailsQuantityUnit);
                    proc.AddIntegerPara("@OrderDetailsPriceLotUnit", OrderDetailsPriceLotUnit);
                    proc.AddVarcharPara("@OrderDetailsRemarks", 100, OrderDetailsRemarks);
                    proc.AddIntegerPara("@CreatedUser", CreatedUser);
                    proc.AddVarcharPara("@OrderDetailsProductDescription", 100, OrderDetailsProductDescription);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int UpdateOrder(int OrderId, int OrderBranch, string OrderDate, string OrderFinYear, string OrderType,
            string OrderContactID, string OrderRefNumber, string OrderAgentID, string OrderInstructions, string OrderPaymentTerm, string OrderPaymentDate,
            string OrderDeliveryDate, string OrderDeliveryAt, int OrderDeliveryBranch, int OrderDeliveryWareHouse, int OrderDeliveryAddress,
            string OrderDeliveryOther, int OrderModifyUser, int OrderPaymentDays, string ParentOrderRefNo)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("sp_UpdatepOrder"))
                {
                    proc.AddIntegerPara("@OrderId", OrderId);
                    if (OrderBranch != 0)
                    {
                        proc.AddIntegerPara("@OrderBranch", OrderBranch);
                    }
                    proc.AddVarcharPara("@OrderDate", 50, OrderDate);
                    proc.AddVarcharPara("@OrderFinYear", 20, OrderFinYear);
                    proc.AddVarcharPara("@OrderType", 20, OrderType);
                    proc.AddVarcharPara("@OrderContactID", 20, OrderContactID);
                    proc.AddVarcharPara("@OrderRefNumber", 50, OrderRefNumber);
                    proc.AddVarcharPara("@OrderAgentID", 50, OrderAgentID);
                    proc.AddVarcharPara("@OrderInstructions", 200, OrderInstructions);
                    proc.AddVarcharPara("@OrderPaymentTerm", 10, OrderPaymentTerm);
                    proc.AddVarcharPara("@OrderPaymentDate", 20, OrderPaymentDate);
                    proc.AddVarcharPara("@OrderDeliveryDate", 20, OrderDeliveryDate);
                    proc.AddVarcharPara("@OrderDeliveryAt", 20, OrderDeliveryAt);
                    if (OrderDeliveryBranch != 0)
                    {
                        proc.AddIntegerPara("@OrderDeliveryBranch", OrderDeliveryBranch);
                    }
                    if (OrderDeliveryWareHouse != 0)
                    {
                        proc.AddIntegerPara("@OrderDeliveryWareHouse", OrderDeliveryWareHouse);
                    }
                    if (OrderDeliveryAddress != 0)
                    {
                        proc.AddIntegerPara("@OrderDeliveryAddress", OrderDeliveryAddress);
                    }
                    proc.AddVarcharPara("@OrderDeliveryOther", 20, OrderDeliveryOther);
                    proc.AddIntegerPara("@OrderModifyUser", OrderModifyUser);
                    proc.AddIntegerPara("@OrderPaymentDays", OrderPaymentDays);
                    proc.AddVarcharPara("@pOrder_ParentOrderNo", 50, ParentOrderRefNo);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetOrderListById(int OrderId)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_GetOrderListById"))
                {
                    proc.AddIntegerPara("@OrderId", OrderId);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetOrderDetailsListById(int OrderDetailsId)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_GetpOrderDetailsListById"))
                {
                    proc.AddIntegerPara("@OrderDetailsId", OrderDetailsId);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetJobWOrkStockListById(int Id)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_GetJobWOrkStockListById"))
                {
                    proc.AddIntegerPara("@Id", Id);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public int InsertJobWorkStock(int JWorkStock_OrderID, string JWorkStock_Number, string JWorkStock_Type, int JWorkStock_ProductID, string JWorkStock_Brand,
            int JWorkStock_Size, int JWorkStock_Color, int JWorkStock_BestBeforeMonth, int JWorkStock_BestBeforeYear, string JWorkStock_ProductDescription,
            string JWorkStock_Quantity, int JWorkStock_QuantityUnit, string JWorkStock_Remarks, int JWorkStock_CreateUser)
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("Sp_InsertJobWorkStock"))
                {
                    proc.AddIntegerPara("@JWorkStock_OrderID", JWorkStock_OrderID);
                    proc.AddVarcharPara("@JWorkStock_Number", 50, JWorkStock_Number);
                    proc.AddVarcharPara("@JWorkStock_Type", 10, JWorkStock_Type);
                    proc.AddIntegerPara("@JWorkStock_ProductID", JWorkStock_ProductID);
                    proc.AddVarcharPara("@JWorkStock_Brand", 50, JWorkStock_Brand); 
                    proc.AddIntegerPara("@JWorkStock_Size", JWorkStock_Size);
                    proc.AddIntegerPara("@JWorkStock_Color", JWorkStock_Color);
                    proc.AddIntegerPara("@JWorkStock_BestBeforeMonth", JWorkStock_BestBeforeMonth);
                    proc.AddIntegerPara("@JWorkStock_BestBeforeYear", JWorkStock_BestBeforeYear);
                    proc.AddVarcharPara("@JWorkStock_ProductDescription", 200, JWorkStock_ProductDescription);
                    proc.AddVarcharPara("@JWorkStock_Quantity", 50, JWorkStock_Quantity);
                    proc.AddIntegerPara("@JWorkStock_QuantityUnit", JWorkStock_QuantityUnit);
                    proc.AddVarcharPara("@JWorkStock_Remarks", 50, JWorkStock_Remarks);
                    proc.AddIntegerPara("@JWorkStock_CreateUser", JWorkStock_CreateUser);
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }
        #endregion

        #region InventoryControlCenter
        public DataTable GetTransactionEditDetailsById(int Id)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_GetTransactionEditDetailsById"))
                {
                    proc.AddIntegerPara("@Id", Id);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetTransactionInsertDetailsById(int Id)
        {
            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("Sp_GetTransactionInsertDetails"))
                {
                    proc.AddIntegerPara("@Id", Id);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }
        #endregion




        public string getUsedMainAccountByProductId(int ProductId)
        {
            string returnValue = "";

            ProcedureExecute proc;
            DataTable retTable;
            try
            {
                using (proc = new ProcedureExecute("prc_ProductMaster_bindData"))
                {
                    proc.AddVarcharPara("@action", 20, "GetUsedMainAccount");
                    proc.AddIntegerPara("@Id", ProductId);
                    retTable = proc.GetTable();
                    if (retTable.Rows.Count > 0) {
                        returnValue = Convert.ToString(retTable.Rows[0][0]);
                    }
                    return returnValue;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }


            return returnValue;
        
        }

    }
}
