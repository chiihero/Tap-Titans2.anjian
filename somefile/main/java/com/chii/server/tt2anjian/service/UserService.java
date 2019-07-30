package com.chii.server.tt2anjian.service;

public interface UserService {

    User getUserInfoByUsername(String username);

    void insertUserInfo(User user);

    void updateUserPasswd(User user);

    void deleteUserInfo(User user);

}
