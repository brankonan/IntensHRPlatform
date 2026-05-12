import { BrowserRouter, Routes, Route } from "react-router-dom";
import CandidatesPage from "./pages/CandidatesPage";
import AddCandidatePage from "./pages/AddCandidatePage";
import CandidateDetailsPage from "./pages/CandidatesDetailsPage";
import SearchPage from "./pages/SearchPage";
import AddSkillPage from "./pages/AddSkillPage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<CandidatesPage />} />
        <Route path="/add" element={<AddCandidatePage />} />
        <Route path="/candidates/:id" element={<CandidateDetailsPage />} />
        <Route path="/search" element={<SearchPage />} />
        <Route path="/add-skill" element={<AddSkillPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
