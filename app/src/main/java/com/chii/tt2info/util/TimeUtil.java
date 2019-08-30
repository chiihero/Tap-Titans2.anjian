package com.chii.tt2info.util;

import java.sql.Timestamp;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class TimeUtil {
    public static Date Timestamp2Data(Long timestamp) {
        Timestamp ts = new Timestamp(timestamp);
        return (Date) ts;
    }

    public static Date String2Data(String timestamp) {
        Timestamp ts = null;
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.CHINA);
        try {
            Date dt = sdf.parse(timestamp);
            ts = new Timestamp(dt.getTime());
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return (Date) ts;
    }
}
