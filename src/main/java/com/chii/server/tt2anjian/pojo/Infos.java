package com.chii.server.tt2anjian.pojo;

import org.springframework.stereotype.Component;

public class Infos {
    private Integer iid;

    private Integer mid;

    private String layer;

    private String usetime;

    public Infos(Integer iid, Integer mid, String layer, String usetime) {
        this.iid = iid;
        this.mid = mid;
        this.layer = layer;
        this.usetime = usetime;
    }

    public Infos() {
        super();
    }

    public Integer getIid() {
        return iid;
    }

    public void setIid(Integer iid) {
        this.iid = iid;
    }

    public Integer getMid() {
        return mid;
    }

    public void setMid(Integer mid) {
        this.mid = mid;
    }

    public String getLayer() {
        return layer;
    }

    public void setLayer(String layer) {
        this.layer = layer == null ? null : layer.trim();
    }

    public String getUsetime() {
        return usetime;
    }

    public void setUsetime(String usetime) {
        this.usetime = usetime == null ? null : usetime.trim();
    }
}