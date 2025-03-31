import { RoboDto } from "../models/roboDto";
import { ApiResponse } from "./apiResponse";

export interface ObterRoboResponse extends ApiResponse {
  robo: RoboDto;
}