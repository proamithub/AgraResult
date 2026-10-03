using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RModel.Enum
{
    public enum ENUM_APP_SEX : int
    {
        Male = 1,
        Female = 2,
        Other = 0
    }
    public enum ENUM_APP_CASTE : int
    {
        GENERAL = 1,
        SC = 2,
        ST = 3,
        OBC = 4,
        MBC = 5,
        MINORITY = 6,
        OTHER = 7,

    }
    public enum ENUM_APP_RELIGION : int
    {


        Buddhist = 7,
        Christian = 4,
        Hindu = 1,
        Jain = 6,
        Muslim = 2,
        NA = 0,
        Parsi = 5,
        Sikh = 3,
    }

    public enum ENUM_APP_BLOODGROUP : int
    {
        [Description("A-")]
        Aminus = 1,
        [Description("A+")]
        Aplus = 2,
        [Description("B-")]
        Bminus = 3,
        [Description("B+")]
        Bplus = 4,
        [Description("AB-")]
        ABminus = 5,
        [Description("AB+")]
        ABplus = 6,
        [Description("O-")]
        Ominus = 7,
        [Description("O+")]
        Oplus = 8,
    }

    public enum ENUM_APP_NATIONALITY : int
    {

        INDIAN = 1,
        AFGHANISTAN = 2,
        NEPAL = 3,
        BANGLADESH = 4,
        BHUTAN = 5,
    }

    public enum ENUM_APP_MOTHERTONGUE : int
    {
        Assamese = 1,
        Bengali = 2,
        Bodo = 3,
        Dogri = 4,
        English = 5,
        Gujrati = 6,
        Hindi = 7,
        Kannada = 8,
        Kashmiri = 9,
        Konkani = 10,
        Maithili = 11,
        Malayalam = 12,
        Manipuri = 13,
        Marathi = 14,
        Nepali = 15,
        Oriya = 16,
        Punjabi = 17,
        Sanskrit = 18,
        Santhali = 19,
        Sindhi = 20,
        Tamil = 21,
        Telugu = 22,
        Urdu = 23,
    }


    public enum ENUM_APP_STATUS: int
    {
        Registration = 1,
        Appliedprogram = 2,
        DocumentUploaded = 3,
        Verifiedapplication = 4,
        Feepaid=5,
        Downloadapplication=6,
        
        CounsellingDocVerified=7,
        Counselling = 8,
        CounsellingFeePAID =9,
        CounsellingLetter=10,
    }
}
