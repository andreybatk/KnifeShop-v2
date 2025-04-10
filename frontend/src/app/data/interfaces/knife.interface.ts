import { KnifeInfo } from "./knife-info.interface";

export interface Knife {
  id: number,
  title: string,
  category: string,
  description: string,
  image: string,
  images: string[],
  price: number,
  isOnSale: boolean,
  createdAt: Date,
  knifeInfo: KnifeInfo
}