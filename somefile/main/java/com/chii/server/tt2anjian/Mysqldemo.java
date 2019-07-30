package com.chii.server.tt2anjian;

import java.sql.*;

public class Mysqldemo {
    static final String JDBC_DRIVER = "com.mysql.cj.jdbc.Driver";
    static final String DB_passwd = "jdbc:mysql://139.199.11.57:3306/tt2anjian?useUnicode=true&characterEncoding=utf-8&useSSL=true&serverTimezone=GMT";
    static final String USER = "tt2anjian";
    static final String PASS = "h6JhSewGwiCrJ6zx";

    public static void main(String[] args) {
        Connection conn = null;
        Statement stmt = null;
        try {
            // 注册 JDBC 驱动
            Class.forName(JDBC_DRIVER);
            // 打开链接
            System.out.println("连接数据库...");
            conn = DriverManager.getConnection(DB_passwd, USER, PASS);

            // 执行查询
            System.out.println(" 实例化Statement对象...");
            stmt = conn.createStatement();
            String sql;
            sql = "SELECT * FROM user";
            ResultSet rs = stmt.executeQuery(sql);

            // 展开结果集数据库
            while (rs.next()) {
                // 通过字段检索
                int id = rs.getInt("uid");
                String username = rs.getString("username");
                String passwd = rs.getString("passwd");
                // 输出数据
                System.out.print("ID: " + id);
                System.out.print(", 站点名称: " + username);
                System.out.print(", 站点 passwd: " + passwd);
                System.out.print("\n");
            }
            // 完成后关闭
            rs.close();
            stmt.close();
            conn.close();
        } catch (SQLException se) {
            // 处理 JDBC 错误
            se.printStackTrace();
        } catch (Exception e) {
            // 处理 Class.forusername 错误
            e.printStackTrace();
        } finally {
            // 关闭资源
            try {
                if (stmt != null) stmt.close();
            } catch (SQLException se2) {
            }// 什么都不做
            try {
                if (conn != null) conn.close();
            } catch (SQLException se) {
                se.printStackTrace();
            }
        }
        System.out.println("Goodbye!");
    }
}