package com.chii.tt2info.connes;

import android.content.Context;
import android.util.Log;

import com.android.volley.AuthFailureError;
import com.android.volley.DefaultRetryPolicy;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.chii.tt2info.MainActivity;
import com.google.gson.Gson;


import java.lang.reflect.Type;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class MyVolley {
//    private static String server_rul= "http://192.168.2.117:8088";
    private static String server_rul= "http://www.chiinas.club:8088";
    public static String infolist_url = server_rul+"/info/getinfolist";
    public static String infos_url = server_rul+"/info/getinfos";


    private static RequestQueue requestQueue = null;
    private static MyVolley volleyHelper;

    public static MyVolley getMyVolley(Context context) {
        if (volleyHelper == null)
            volleyHelper = new MyVolley();
        requestQueue = Volley.newRequestQueue(context);
        return volleyHelper;
    }
    /**
     * post请求
     *
     * @param url,HashMap,volleyInterface,Class
     * @return bean
     */
    public void Post(String url, final HashMap<String, String> hashMap,
                         final volleyInterface volleyInterface) {
        Log.i(MainActivity.TAG, "开始请求");
        StringRequest stringRequest = new StringRequest(Request.Method.POST, url,
                new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                Log.d(MainActivity.TAG, "onResponse: "+response);
                volleyInterface.ResponseResult(response);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError volleyError) {
                volleyInterface.ResponError(volleyError);
                Log.i(MainActivity.TAG, "请求错误");
            }

        }) {
            @Override
            protected Map<String, String> getParams() throws AuthFailureError {
                return hashMap;
            }
        };

        AddrequestQueue(stringRequest);
    }

    /**
     * GET请求 url,volleyInterface,Class
     * 以实体类形式返回，然后进行强转
     **/
    public void Get(String url, HashMap<String, String> hashMap,
                        final volleyInterface volleyInterface) {
        String str = paramsCastUrl(url, hashMap);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, str,
                new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                Log.d(MainActivity.TAG, "onResponse: "+response);
                volleyInterface.ResponseResult(response);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError volleyError) {
                volleyInterface.ResponError(volleyError);
                Log.d(MainActivity.TAG, "请求错误");
            }
        });
        AddrequestQueue(stringRequest);
    }



    //此方法是 Volley配置方法
    private void AddrequestQueue(StringRequest req) {
        // 设置超时时间
        req.setRetryPolicy(new DefaultRetryPolicy(3 * 1000, 1, 1.0f));
        // 是否开启缓存；
        req.setShouldCache(true);
        // 将请求加入队列
        requestQueue.add(req);
        // 开始发起请求
        requestQueue.start();
    }

    /**
     * 把map参数 拼接成 get请求的 url格式 ,最后和 传过来的url一起拼接
     */
    public static String paramsCastUrl(String url, Map<String, String> map) {
        if (map != null) {
            String params = "?";
            /** 遍历map，把 键值对应 */
            for (Map.Entry<String, String> entry : map.entrySet()) {
                params += entry.getKey() + "=" + entry.getValue() + "&";
            }
            /** 把一个字符串 从 0 一直截取到 字符串减一个长度处 */
            params = params.substring(0, params.length() - 1);
            return url + params;
        }
        return url;
    }

}
