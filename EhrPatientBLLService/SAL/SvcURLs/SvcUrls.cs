﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;

namespace SAL.SvcURLs
{
    public static class SvcUrls
    {
        public static string urlCheckPatientLogin = UtilityLibrary.GetAppSettingValue("urlCheckPatientLogin");
        public static string urlCheckStaffLogin = UtilityLibrary.GetAppSettingValue("urlCheckStaffLogin");
        public static string urlDeletePatient = UtilityLibrary.GetAppSettingValue("urlDeletePatient");
        public static string urlGetPatientByPIN = UtilityLibrary.GetAppSettingValue("urlGetPatientByPIN");
        public static string urlGetPatientByID = UtilityLibrary.GetAppSettingValue("urlGetPatientByID");
        public static string urlGetNextPatientID = UtilityLibrary.GetAppSettingValue("urlGetNextPatientID");
        public static string urlGetPatientCollection = UtilityLibrary.GetAppSettingValue("urlGetPatientCollection");
        public static string urlInsertPatient = UtilityLibrary.GetAppSettingValue("urlInsertPatient");
        public static string urlUpdatePatient = UtilityLibrary.GetAppSettingValue("urlUpdatePatient");
    }
}