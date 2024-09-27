using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_IDENTIFICATION_TOOL.Enums
{
    public enum IntNumericValues
    {
        random_number,//int
        postcode,//int
        zipcode,//int
        
    }
    public enum FloatNumericValues
    {
        latitude,//float
        longitude//float
    }
    public enum StringValues
    {
        name,
        first_name,
        last_name,
        address,
        street_address,
        city,
        state,
        phone_number,
        email,
        ssn,
        license_plate,
        url,
        ipv4,
        image_url,
        id,
        gender,
        marital_status,
        word,
        sentence,
        bank_account_number

    }

    public enum DateTimeValues
    {
        date_of_birth,
        date_this_century

    }
    public enum All
    {
        name,
        first_name,
        last_name,
        address,
        street_address,
        city,
        state,
        postcode,
        zipcode,
        latitude,
        longitude,
        date_of_birth,
        date_this_century,
        phone_number,
        email,
        ssn,
        random_number,
        license_plate,
        url,
        ipv4,
        image_url,
        id,
        gender,
        marital_status,
        word,
        sentence,
        bank_account_number,
    }
}
