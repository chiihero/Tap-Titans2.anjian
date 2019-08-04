package com.chii.tt2info.pojo;

import java.util.Date;

public class Info {
    private Integer mid;

    private String title;

    private String notes;

    private Integer layerSet;

    private Integer updateAll;

    private Integer updateMini;

    private String time;

    private String username;

    public Info(Integer mid, String title, String notes, Integer layerSet, Integer updateAll, Integer updateMini, String time, String username) {
        this.mid = mid;
        this.title = title;
        this.notes = notes;
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

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title == null ? null : title.trim();
    }

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes == null ? null : notes.trim();
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

    public String getTime() {
        return time;
    }
    public void setTime(String time) {
        this.time = time;
    }
    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username == null ? null : username.trim();
    }
}