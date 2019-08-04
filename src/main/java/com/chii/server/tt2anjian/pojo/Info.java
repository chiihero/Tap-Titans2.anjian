package com.chii.server.tt2anjian.pojo;

import java.sql.Timestamp;
import java.util.Date;

public class Info {
    private Integer mid;

    private Integer layerSet;

    private Integer updateAll;

    private Integer updateMini;

    private Timestamp time;

    private String username;

    public Info(Integer mid, Integer layerSet, Integer updateAll, Integer updateMini, Timestamp time, String username) {
        this.mid = mid;
        this.layerSet = layerSet;
        this.updateAll = updateAll;
        this.updateMini = updateMini;
        this.time = time;
        this.username = username;
    }

    public Info() {
        super();
    }

    public Integer getMid() {
        return mid;
    }

    public void setMid(Integer mid) {
        this.mid = mid;
    }

    public Integer getLayerSet() {
        return layerSet;
    }

    public void setLayerSet(Integer layerSet) {
        this.layerSet = layerSet;
    }

    public Integer getUpdateAll() {
        return updateAll;
    }

    public void setUpdateAll(Integer updateAll) {
        this.updateAll = updateAll;
    }

    public Integer getUpdateMini() {
        return updateMini;
    }

    public void setUpdateMini(Integer updateMini) {
        this.updateMini = updateMini;
    }

    public Timestamp getTime() {
        return time;
    }

    public void setTime(Timestamp time) {
        this.time = time;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username == null ? null : username.trim();
    }
}