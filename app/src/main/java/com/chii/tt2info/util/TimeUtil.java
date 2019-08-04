package com.chii.tt2info.util;

import java.sql.Timestamp;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class TimeUtil {
    public static Date Timestamp2Data(Long timestamp){
        Timestamp ts = new Timestamp(timestamp);
        return (Date)ts;
    }

}
