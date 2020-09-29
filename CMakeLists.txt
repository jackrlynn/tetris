cmake_minimum_required(VERSION 3.17)
project(tetris)

set(CMAKE_CXX_STANDARD 14)

add_executable(tetris main.cpp piece.cpp piece.h)