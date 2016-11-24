﻿using Garage_2._5.Conversions;
using Garage_2._5.DAL;
using Garage_2._5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Garage_2._5.Repositories
{
    public class GarageRepo
    {
        public GarageContext GContext { get; set; }

        public GarageRepo()
        {
            GContext = new GarageContext();
        }

        public Vehicle GetVehicleById(int id)
        {
            return GContext.Vehicles.Find(id);
        }

        public IEnumerable<VehicleViewModel> GetVehicleList()
        {
            List<VehicleViewModel> VModelList = GContext.Vehicles.ConvertToVehicleViewModel();

            return VModelList.ToList();
        }

        public IEnumerable<VehicleDetailsViewModel> GetVehiclesDetailsList()
        {
            List<VehicleDetailsViewModel> VDModelList = GContext.Vehicles.ConvertToVehicleDetailsModel();

            return VDModelList;
        }

        public IEnumerable<VehicleViewModel> SearchInIndex(int Type, string RegNr)
        {
            List<VehicleViewModel> VModelList = GContext.Vehicles.Where(v =>
                Type == 0 ? true : v.VehicleType.TypeId == Type &&
                v.RegNr.Contains(RegNr)
                ).ToList().ConvertToVehicleViewModel();

            return VModelList;
        }

        public IEnumerable<VehicleDetailsViewModel> SearchInDetails(int Type, string RegNr)
        {
            List<VehicleDetailsViewModel> VModelList = GContext.Vehicles.Where(v =>
                Type == 0 ? true : v.VehicleType.TypeId == Type &&
                v.RegNr.Contains(RegNr)
                ).ToList().ConvertToVehicleDetailsModel();

            return VModelList;
        }

        // should be working
        public void AddVehicle(VehicleCreateViewModel VCVModel)
        {
            Vehicle toAdd = VCVModel.ConvertyToVehicleFromCreateModel();
            toAdd.Owner = GContext.Owners.Find(toAdd.PNR);
            toAdd.VehicleType = GContext.VehicleTypes.Find(toAdd.TypeId);
            GContext.Vehicles.Add(toAdd);
            GContext.SaveChanges();
        }
    }
}