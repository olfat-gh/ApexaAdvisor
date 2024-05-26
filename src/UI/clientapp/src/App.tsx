import React, { useState, useEffect } from "react";
import "./App.css";
import DataGrid from "./components/DataGrid";
import { IAdvisor, IResponse } from "./models/IAdvisor";
import Api from "./services/api";
import { PAGE_SIZE } from "./constants/index";

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

  return (
    <div className="App">
      <DataGrid
        totalPages={data?.totalPages || 0}
        data={data?.records}
        loading={loading}
        onFetch={fetch}
        onDelete={handleDelete}
      />
    </div>
  );
}

export default App;
