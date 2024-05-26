enum HealthType {
  Green = "Green",
  Yellow = "Yellow",
  Red = "Red",
}

export interface IAdvisor {
  id: string;
  name: string;
  sin: string;
  address: string;
  phone: string;
  status: HealthType;
}

export interface IPayload {
  name: string;
  sin: string;
  address: string;
  phone: string;
}

export interface IResponse {
  totalPages: number,
  records: IAdvisor[]
}

