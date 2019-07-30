package com.chii.server.tt2anjian.service;

import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.Infos;

import java.util.List;

public interface InfosService {

    List<Infos> getInfosByMid(int mid);

    void insertInfos(Infos infos);

}
