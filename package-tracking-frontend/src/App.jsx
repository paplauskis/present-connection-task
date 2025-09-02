import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import PackageListPage from './pages/PackageListPage'
import PackageDetailsPage from './pages/PackageDetailsPage'
import CreatePackagePage from './pages/CreatePackagePage'

function App() {
  return (
    <Router>
      <Routes>
        <Route path='/' element={<PackageListPage />} />
        <Route path='/package/:trackingNumber' element={<PackageDetailsPage />} />
        <Route path='/create' element={<CreatePackagePage />} />
      </Routes>
    </Router>
  )
}

export default App
