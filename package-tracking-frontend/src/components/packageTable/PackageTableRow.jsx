import StatusDropdown from '../utils/AvailableStatusDropdown'
import { useNavigate } from 'react-router-dom'

export default function PackageTableRow({ pkg, onStatusChange }) {
  const navigate = useNavigate()

  return (
    <tr className="package-table-row" onClick={() => navigate(`/package/${pkg.trackingNumber}`)}>
      <td className={pkg.status}>{pkg.trackingNumber}</td>
      <td>{pkg.sender.name}</td>
      <td>{pkg.sender.address}</td>
      <td>{pkg.sender.phone}</td>
      <td>{pkg.recipient.name}</td>
      <td>{pkg.recipient.address}</td>
      <td>{pkg.recipient.phone}</td>
      <td>{pkg.status}</td>
      <td>{new Date(pkg.createdAt).toLocaleString()}</td>
      <td onClick={(e) => e.stopPropagation()}>
        <StatusDropdown
          trackingNumber={pkg.trackingNumber}
          availableStatusTransitions={pkg.availableStatusTransitions}
          onStatusChange={onStatusChange}
        />
      </td>
    </tr>
  )
}