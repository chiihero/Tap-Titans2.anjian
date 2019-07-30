package com.chii.server.tt2anjian.mapper;

import com.chii.server.tt2anjian.pojo.Info;

public interface InfoMapper {
    int deleteByPrimaryKey(Integer mid);

    int insert(Info record);

    int insertSelective(Info record);

    Info selectByPrimaryKey(Integer mid);

    int updateByPrimaryKeySelective(Info record);

    int updateByPrimaryKey(Info record);
}