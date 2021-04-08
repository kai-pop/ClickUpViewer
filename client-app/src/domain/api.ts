import { Space, Folder, List, GetTaskParameter, Task } from "@/@types/clickUp";
import axios from "axios";

export class Api {
  private readonly URL = "https://api.clickup.com/api/v2";

  constructor(private readonly token: string) {
    if (!this.token.length) throw new Error("token is empty.");
  }

  private get<T>(path: string, params: object = {}) {
    const queryString =
      "?" +
      Object.entries(params)
        .map((e) => {
          const key = e[0];
          const value = e[1];
          if (Array.isArray(value)) {
            return value
              .map((v, i) => {
                const paramKey = encodeURIComponent(`${key}[${i}]`);
                const paramValue = encodeURIComponent(v);
                return `${paramKey}=${paramValue}`;
              })
              .join("&");
          } else {
            return `${key}=${value}`;
          }
        })
        .join("&");

    const url = `${URL}/${path}${queryString}`;
    return axios
      .get(url, {
        headers: {
          Authorization: this.token,
          "Content-Type": "application/json",
        },
      })
      .then((res) => res.data as T);
  }

  async getSpaces(teamId: number, archived = false) {
    return await this.get<Space[]>(`/team/${teamId}/space`, {
      archived: archived,
    });
  }

  async getFolders(spaceId: number, archived = false) {
    return await this.get<Folder[]>(`/space/${spaceId}/folder`, {
      archived: archived,
    });
  }

  async getLists(folderId: number, archived = false) {
    return await this.get<List[]>(`/folder/${folderId}/list`, {
      archived: archived,
    });
  }

  async getTasks(listId: number, params: Partial<GetTaskParameter>) {
    return await this.get<Task[]>(`list/${listId}/task`, params);
  }
}
