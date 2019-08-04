package com.chii.tt2info.util;

import java.sql.Timestamp;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class demo {

    public static void main(String[] args) {
//        String time = "2019-07-31T20:01:40.000+0000";
//        Timestamp timestamp = null;
//        System.out.println(time);
//        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss",Locale.CHINA);
//        try {
//            Date dt = sdf.parse(time);
//            timestamp = new Timestamp(dt.getTime());
//        } catch (ParseException e) {
//            e.printStackTrace();
//        }
//        System.out.println(timestamp);
        long time = 1564598022000L;

        Timestamp timestamp = new Timestamp(time);
        System.out.println(timestamp);

    }
}
