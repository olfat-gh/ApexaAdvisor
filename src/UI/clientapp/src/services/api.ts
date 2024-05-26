import axios from "axios";
import { IAdvisor, IResponse, IPayload } from "../models/interfaces";
import { URL } from "../constants";

class Api {
  async getAdvisorsList(pageSize: number, pageIndex: number) {
    const response = await axios.get<IResponse>(URL.URL_GET_LIST_ADVISORS, {
      params: {
        PageSize: pageSize,
        PageIndex: pageIndex,
      },
    });
    return response.data;
  }

  async delAdvisor(id: string) {
    const response = await axios.delete(
      URL.URL_DELETE_ADVISOR.replace("{advisorId}", id)
    );
    return response.data;
  }

  async addAdvisor(advisor: IPayload) {
    const response = await axios.post<IAdvisor[]>(URL.URL_ADD_ADVISOR, advisor);
    return response.data;
  }
}

export default new Api();
