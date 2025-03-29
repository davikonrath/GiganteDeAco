import { BracoDto } from "./bracoDto";
import { CabecaDto } from "./cabecaDto";

export interface RoboDto {
    cabeca: CabecaDto;
    bracoEsquerdo: BracoDto;
    bracoDireito: BracoDto;
}