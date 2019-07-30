package com.chii.server.tt2anjian.service;

import java.util.List;

public interface InfosService {

    List<Infos> getInfosByMid(int mid);

    void insertInfos(Infos infos);

}
