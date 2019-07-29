package com.chii.server.tt2anjian.service;

import com.chii.server.tt2anjian.pojo.User;

public interface UserService {

    User getUserInfoByUsername(String username);

    void insertUserInfo(User user);

    void updateUserPasswd(User user);

    void deleteUserInfo(User user);

}
