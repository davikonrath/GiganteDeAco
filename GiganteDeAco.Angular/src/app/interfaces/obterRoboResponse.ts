import { ApiResponse } from "./apiResponse";
import { RoboDto } from "../models/roboDto";

export interface ObterRoboResponse extends ApiResponse {
    robo: RoboDto;
  }