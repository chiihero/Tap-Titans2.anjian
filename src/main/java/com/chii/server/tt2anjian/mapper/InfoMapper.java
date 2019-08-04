package com.chii.server.tt2anjian.mapper;

import com.chii.server.tt2anjian.pojo.Info;
import org.apache.ibatis.annotations.Mapper;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface InfoMapper {
    int deleteByPrimaryKey(Integer mid);

    int insert(Info record);

    int insertSelective(Info record);

    Info selectByPrimaryKey(Integer mid);

    List<Info> selectByUserName(String username);

    int updateByPrimaryKeySelective(Info record);

    int updateByPrimaryKey(Info record);
}