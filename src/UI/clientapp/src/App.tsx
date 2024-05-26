import React, { useState, useEffect } from "react";
import "./App.css";
import DataGrid from "./components/DataGrid";
import { IAdvisor, IPayload, IResponse } from "./models/interfaces";
import Api from "./services/api";
import { PAGE_SIZE } from "./constants/index";
import AddAdvisor from "./components/AddAdvisor";

function App() {
  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<IResponse>();

  const fetch = async (pageIndex: number) => {
    setLoading(true);
    try {
      const response = await Api.getAdvisorsList(PAGE_SIZE, pageIndex);
      setData(response);
    } catch (error) {
      console.log(error);
    }
    setLoading(false);
  };

  const handleDelete = async (id: string) => {
    await Api.delAdvisor(id);
    await fetch(1);
  };
  const handleSubmit = async (payload: IPayload) => {
    try {
      await Api.addAdvisor(payload);
      await fetch(1);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="App">
      <DataGrid
        totalPages={data?.totalPages || 0}
        data={data?.records}
        loading={loading}
        onFetch={fetch}
        onDelete={handleDelete}
      />
      <AddAdvisor onAddAdvisor={handleSubmit} />
    </div>
  );
}

export default App;
