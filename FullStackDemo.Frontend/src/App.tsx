import { useState, useEffect } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import SideBar from "@components/layout/SideBar";
import TopBar from "@components/layout/TopBar";
import PageFooter from "@components/layout/PageFooter";
import MobileSuitsPage from "@pages/MobileSuits/MobileSuitsPage";
import AboutPage from "@pages/About/AboutPage";
import { GetJwtToken } from "@services/AuthenticationService";
import type { IJwtToken } from "@/types/common/IJwtToken";

export default function App() {
  const [sidebarOpen, setSidebarOpen] = useState(false);

  useEffect(() => {
    const existing = localStorage.getItem("bearer");
    if (!existing) {
      GetJwtToken().then((res) => {
        if (res.success) {
          localStorage.setItem("bearer", JSON.stringify(res.data as IJwtToken));
        }
      });
    }
  }, []);

  return (
    <div className="flex h-screen overflow-hidden bg-background">
      <SideBar open={sidebarOpen} onClose={() => setSidebarOpen(false)} />
      <div className="flex flex-1 flex-col overflow-hidden">
        <TopBar onToggleSidebar={() => setSidebarOpen((o) => !o)} />
        <main className="flex-1 overflow-y-auto">
          <Routes>
            <Route path="/" element={<Navigate to="/MobileSuits" replace />} />
            <Route path="/MobileSuits" element={<MobileSuitsPage />} />
            <Route path="/About" element={<AboutPage />} />
          </Routes>
        </main>
        <PageFooter />
      </div>
    </div>
  );
}
