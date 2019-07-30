package com.chii.server.tt2anjian.service;

import java.util.List;

public interface InfoService {

    List<Info> getInfoInfoByUsername(String username);

//    List<Info> getInfoLastByUsername(String username);


    int insertInfo(Info info);

    void deleteInfo(int mid);

}
