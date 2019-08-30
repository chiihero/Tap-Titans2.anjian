package com.chii.tt2info.connes;

import com.android.volley.VolleyError;

import java.util.List;

public interface volleyInterface {
    void ResponseResult(String jsonObject);

    void ResponError(VolleyError volleyError);
}
